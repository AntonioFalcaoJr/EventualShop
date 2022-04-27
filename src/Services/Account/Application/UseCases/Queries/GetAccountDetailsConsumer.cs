using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Accounts;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetAccountDetailsConsumer :
    IConsumer<Query.GetAccount>,
    IConsumer<Query.GetAccounts>
{
    private readonly IProjectionRepository<Projection.Account> _repository;

    public GetAccountDetailsConsumer(IProjectionRepository<Projection.Account> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetAccount> context)
    {
        var account = await _repository.GetAsync(context.Message.AccountId, context.CancellationToken);

        await (account is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(account));
    }

    public async Task Consume(ConsumeContext<Query.GetAccounts> context)
    {
        var accounts = await _repository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (accounts is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(accounts));
    }
}