using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Account;
using MassTransit;

namespace Application.UseCases.Queries;

public class ListAddressesConsumer : IConsumer<Query.ListAddresses>
{
    private readonly IProjectionRepository<Projection.Address> _repository;

    public ListAddressesConsumer(IProjectionRepository<Projection.Address> repository)
        => _repository = repository;

    public async Task Consume(ConsumeContext<Query.ListAddresses> context)
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