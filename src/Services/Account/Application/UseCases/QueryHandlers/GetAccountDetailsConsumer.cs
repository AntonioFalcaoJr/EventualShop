using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetAccountDetailsConsumer :
    IConsumer<Query.GetAccount>,
    IConsumer<Query.GetAccounts>
{
    private readonly IProjectionRepository<Projection.Account> _projectionRepository;

    public GetAccountDetailsConsumer(IProjectionRepository<Projection.Account> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetAccount> context)
    {
        var account = await _projectionRepository.GetAsync(context.Message.AccountId, context.CancellationToken);

        await (account is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(account));
    }

    public async Task Consume(ConsumeContext<Query.GetAccounts> context)
    {
        var accounts = await _projectionRepository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (accounts is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(accounts));
    }
}