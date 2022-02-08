using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Account;
using MassTransit;
using GetAccountsQuery = ECommerce.Contracts.Account.Queries.GetAccounts;

namespace Application.UseCases.Queries;

public class GetAccountsDetailsWithPaginationConsumer : IConsumer<GetAccountsQuery>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountsDetailsWithPaginationConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetAccountsQuery> context)
    {
        var paginatedResult = await _projectionsService.GetAccountsAsync(context.Message.Paging, context.CancellationToken);
        await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
    }
}