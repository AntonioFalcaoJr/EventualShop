using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetAccountsConsumer : IConsumer<Query.GetAccounts>
{
    private readonly IProjectionRepository<Projection.Account> _repository;

    public GetAccountsConsumer(IProjectionRepository<Projection.Account> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetAccounts> context)
    {
        var accounts = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(accounts switch
        {
            {Page.Size: > 0} => accounts,
            {Page.Size: < 1} => new Reply.NoContent(),
            _ => new Reply.NotFound()
        });
    }
}