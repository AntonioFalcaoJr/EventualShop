using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetAccountDetailsConsumer :
    IConsumer<Queries.GetAccountDetails>,
    IConsumer<Queries.GetAccounts>
{
    private readonly IProjectionRepository<Projections.Account> _projectionRepository;

    public GetAccountDetailsConsumer(IProjectionRepository<Projections.Account> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetAccountDetails> context)
    {
        var account = await _projectionRepository.GetAsync(context.Message.AccountId, context.CancellationToken);

        await (account is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(account));
    }

    public async Task Consume(ConsumeContext<Queries.GetAccounts> context)
    {
        var accounts = await _projectionRepository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (accounts is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(accounts));
    }
}