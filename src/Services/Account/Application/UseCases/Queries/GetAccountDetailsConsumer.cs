using Application.EventSourcing.Projections;
using ECommerce.Contracts.Account;
using MassTransit;
using GetAccountDetailsQuery = ECommerce.Contracts.Account.Queries.GetAccountDetails;

namespace Application.UseCases.Queries;

public class GetAccountDetailsConsumer : IConsumer<GetAccountDetailsQuery>
{
    private readonly IAccountProjectionsService _projectionsService;

    public GetAccountDetailsConsumer(IAccountProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetAccountDetailsQuery> context)
    {
        var accountDetails = await _projectionsService.GetAccountDetailsAsync(context.Message.AccountId, context.CancellationToken);
        await context.RespondAsync<Responses.AccountDetails>(accountDetails);
    }
}