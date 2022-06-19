using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetAccountConsumer : IConsumer<Query.GetAccount>
{
    private readonly IProjectionRepository<Projection.Account> _repository;

    public GetAccountConsumer(IProjectionRepository<Projection.Account> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<Query.GetAccount> context)
    {
        var account = await _repository.GetAsync(context.Message.AccountId, context.CancellationToken);

        await (account is null
            ? context.RespondAsync<Reply.NotFound>(new())
            : context.RespondAsync(account));
    }
}