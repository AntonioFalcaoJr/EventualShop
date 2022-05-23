using Contracts.Abstractions.Paging;
using Contracts.Services.Account;
using MassTransit;
using Microsoft.AspNetCore.Http.HttpResults;
using WebAPI.Abstractions;

namespace WebAPI;

public static class Accounts
{
    public static GroupRouteBuilder MapAccountApi(this GroupRouteBuilder group)
    {
        group.MapGet("/", GetAllAsync)
            .ProducesValidationProblem()
            .ProducesProblem(StatusCodes.Status408RequestTimeout)
            .ProducesProblem(StatusCodes.Status500InternalServerError);

        return group;
    }

    private static Task<Results<Ok<IPagedResult<Projection.Account>>, NoContent, NotFound, Problem>> GetAllAsync(IBus bus, ushort? limit, ushort? offset, CancellationToken ct)
        => MessageBus.GetPagedProjectionAsync<Query.GetAccounts, Projection.Account>(bus, new(limit, offset), ct);
}