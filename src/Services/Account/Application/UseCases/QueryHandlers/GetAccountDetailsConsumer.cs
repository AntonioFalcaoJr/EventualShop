using Application.EventSourcing.Projections;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetAccountDetailsConsumer : IConsumer<Queries.GetAccountDetails>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountDetailsConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetAccountDetails> context)
    {
        var accountDetails = await _projectionsService.GetAccountDetailsAsync(context.Message.AccountId, context.CancellationToken);
        await context.RespondAsync<Responses.Account>(accountDetails);
    }
}