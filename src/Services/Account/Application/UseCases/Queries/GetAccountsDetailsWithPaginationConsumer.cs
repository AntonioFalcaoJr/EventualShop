using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Account;
using MassTransit;
using GetAccountsDetailsWithPaginationQuery = ECommerce.Contracts.Account.Queries.GetAccountsDetailsWithPagination;

namespace Application.UseCases.Queries;

public class GetAccountsDetailsWithPaginationConsumer : IConsumer<GetAccountsDetailsWithPaginationQuery>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountsDetailsWithPaginationConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetAccountsDetailsWithPaginationQuery> context)
    {
        var paginatedResult = await _projectionsService.GetAccountsDetailsWithPaginationAsync(
            paging: new() { Limit = context.Message.Limit, Offset = context.Message.Offset },
            predicate: _ => true,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
    }
}