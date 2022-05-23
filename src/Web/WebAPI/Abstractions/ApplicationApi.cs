using Contracts.Abstractions;
using Contracts.Abstractions.Paging;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WebAPI.Abstractions;

public static class ApplicationApi
{
    public static void MapQuery(this IEndpointRouteBuilder endpoints, string pattern, Delegate handler)
        => endpoints
            .MapGet(pattern, handler)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status408RequestTimeout)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

    public static void MapCommand(this IEndpointRouteBuilder endpoints, Func<IEndpointRouteBuilder, RouteHandlerBuilder> action)
        => action(endpoints).ProducesValidationProblem();

    public static async Task<AcceptedAtRoute> SendCommandAsync<TCommand>(IBus bus, TCommand command, CancellationToken cancellationToken)
        where TCommand : class, ICommand
    {
        var endpoint = await bus.GetSendEndpoint(Address<TCommand>());
        await endpoint.Send(command, cancellationToken);
        return TypedResults.AcceptedAtRoute();
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