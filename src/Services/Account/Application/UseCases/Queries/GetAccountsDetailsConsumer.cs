using Application.EventSourcing.Projections;
using ECommerce.Contracts.Account;
using MassTransit;
using GetAccountsQuery = ECommerce.Contracts.Account.Queries.GetAccounts;

namespace Application.UseCases.Queries;

public class GetAccountsDetailsConsumer : IConsumer<GetAccountsQuery>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountsDetailsConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetAccountsQuery> context)
    {
        var paginatedResult = await _projectionsService.GetAccountsAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);
        await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
    }
}