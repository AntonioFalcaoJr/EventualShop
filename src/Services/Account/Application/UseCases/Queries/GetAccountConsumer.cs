using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetAccountConsumer :
    IConsumer<Query.GetAccount>,
    IConsumer<Query.GetAccounts>
{
    private readonly IProjectionRepository<Projection.Account> _repository;

    public GetAccountConsumer(IProjectionRepository<Projection.Account> repository)
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
        var accounts = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(accounts switch
        {
            {PageInfo.Size: > 0} => accounts,
            {PageInfo.Size: <= 0} => new NoContent(),
            _ => new NotFound()
        });
    }
}