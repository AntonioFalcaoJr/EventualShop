using Contracts.Abstractions;
using Contracts.Abstractions.Messages;
using Contracts.Abstractions.Paging;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Abstractions;

public static class ApplicationApi
{
    public static async Task<AcceptedAtRoute> SendCommandAsync<TCommand>(IBus bus, TCommand command, CancellationToken cancellationToken)
        where TCommand : class, ICommand
    {
        var endpoint = await bus.GetSendEndpoint(Address<TCommand>());
        await endpoint.Send(command, cancellationToken);
        return TypedResults.AcceptedAtRoute();
    }

    public static async Task<Results<Accepted, ValidationProblem>> SendCommandAsync<TCommand>(IRequest request)
        where TCommand : ICommand
    {
        return request.IsValid(out var errors) ? await AcceptAsync() : TypedResults.ValidationProblem(errors);

        async Task<Accepted> AcceptAsync()
        {
            var endpoint = await request.Bus.GetSendEndpoint(Address<TCommand>());
            await endpoint.Send(request.AsCommand(), request.CancellationToken);
            return TypedResults.Accepted("");
        }
    }

    public static Task<Results<Ok<TProjection>, NoContent, NotFound, Problem>> GetProjectionAsync<TQuery, TProjection>
        (IBus bus, TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TProjection : class, IProjection
        => GetResponseAsync<TQuery, TProjection>(bus, query, cancellationToken);

    public static Task<Results<Ok<IPagedResult<TProjection>>, NoContent, NotFound, Problem>> GetPagedProjectionAsync<TQuery, TProjection>
        (IBus bus, TQuery query, CancellationToken cancellationToken)
        where TQuery : class, IQuery
        where TProjection : class, IProjection
        => GetResponseAsync<TQuery, IPagedResult<TProjection>>(bus, query, cancellationToken);

    private static async Task<Results<Ok<TProjection>, NoContent, NotFound, Problem>> GetResponseAsync<TQuery, TProjection>
        (IBus bus, TQuery query, CancellationToken ct)
        where TQuery : class, IQuery
        where TProjection : class
    {
        var response = await bus
            .CreateRequestClient<TQuery>(Address<TQuery>())
            .GetResponse<TProjection, Reply.NoContent, Reply.NotFound>(query, ct);

        return response.Message switch
        {
            TProjection projection => TypedResults.Ok(projection),
            Reply.NoContent => TypedResults.NoContent(),
            Reply.NotFound => TypedResults.NotFound(),
            _ => new Problem()
        };
    }

    private static Uri Address<T>()
        => new($"exchange:{KebabCaseEndpointNameFormatter.Instance.SanitizeName(typeof(T).Name)}");
}