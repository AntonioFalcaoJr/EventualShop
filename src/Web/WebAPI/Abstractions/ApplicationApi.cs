using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Protobuf;
using Grpc.Core;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.AspNetCore.Http.TypedResults;
using NoContent = Microsoft.AspNetCore.Http.HttpResults.NoContent;
using NotFound = Microsoft.AspNetCore.Http.HttpResults.NotFound;

namespace WebAPI.Abstractions;

public static class ApplicationApi
{
    public static async Task<Results<Ok<TResponse>, ValidationProblem>> QueryAsync<TClient, TResponse>
        (IQueryRequest<TClient> request, Func<TClient, CancellationToken, AsyncUnaryCall<TResponse>> query)
        where TClient : ClientBase<TClient>
        => request.IsValid(out var errors)
            ? Ok(await query(request.Client, request.CancellationToken))
            : ValidationProblem(errors);

    public static async Task<Results<Ok<TResponse>, NotFound, ValidationProblem, Problem>> GetAsync<TClient, TResponse>
        (IQueryRequest<TClient> request, Func<TClient, CancellationToken, AsyncUnaryCall<GetResponse>> query)
        where TClient : ClientBase<TClient>
        where TResponse : Google.Protobuf.IMessage, new()
    {
        return request.IsValid(out var errors) ? await ResponseAsync() : ValidationProblem(errors);

        async Task<Results<Ok<TResponse>, NotFound, ValidationProblem, Problem>> ResponseAsync()
        {
            var response = await query(request.Client, request.CancellationToken);

            return response.OneOfCase switch
            {
                GetResponse.OneOfOneofCase.NotFound => NotFound(),
                GetResponse.OneOfOneofCase.Projection when response.Projection.TryUnpack<TResponse>(out var result) => Ok(result),
                _ => new Problem()
            };
        }
    }

    public static async Task<Results<Ok<PagedResult<TResponse>>, NoContent, ValidationProblem, Problem>> ListAsync<TClient, TResponse>
        (IQueryRequest<TClient> request, Func<TClient, CancellationToken, AsyncUnaryCall<ListResponse>> query)
        where TClient : ClientBase<TClient>
        where TResponse : Google.Protobuf.IMessage, new()
    {
        return request.IsValid(out var errors) ? await ResponseAsync() : ValidationProblem(errors);

        async Task<Results<Ok<PagedResult<TResponse>>, NoContent, ValidationProblem, Problem>> ResponseAsync()
        {
            var response = await query(request.Client, request.CancellationToken);

            return response.OneOfCase switch
            {
                ListResponse.OneOfOneofCase.NoContent => NoContent(),
                ListResponse.OneOfOneofCase.PagedResult => Ok<PagedResult<TResponse>>(response.PagedResult),
                _ => new Problem()
            };
        }
    }

    public static async Task<Results<Accepted, ValidationProblem>> SendCommandAsync<TCommand>(ICommandRequest request)
        where TCommand : class, ICommand
    {
        return request.IsValid(out var errors) ? await AcceptAsync() : ValidationProblem(errors);

        async Task<Accepted> AcceptAsync()
        {
            var endpoint = await request.Bus.GetSendEndpoint(Address<TCommand>());
            await endpoint.Send((TCommand)request.Command, request.CancellationToken);
            return Accepted("");
        }
    }

    private static Uri Address<T>()
        => new($"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}