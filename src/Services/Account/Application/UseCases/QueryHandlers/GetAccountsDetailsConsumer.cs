using Application.EventSourcing.Projections;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetAccountsDetailsConsumer : IConsumer<Queries.GetAccounts>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountsDetailsConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetAccounts> context)
    {
        var paginatedResult = await _projectionsService.GetAccountsAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);
        await context.RespondAsync<Responses.AccountsDetailsPagedResult>(paginatedResult);
    }
}