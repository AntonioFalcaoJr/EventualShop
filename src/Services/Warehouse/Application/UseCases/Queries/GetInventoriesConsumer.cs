using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetInventoriesConsumer : IConsumer<Query.GetInventories>
{
    private readonly IProjectionRepository<Projection.Inventory> _repository;

    public GetInventoriesConsumer(IProjectionRepository<Projection.Inventory> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetInventories> context)
    {
        var inventory = await _repository.GetAllAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(inventory switch
        {
            {PageInfo.Size: > 0} => inventory,
            {PageInfo.Size: <= 0} => new NoContent(),
            _ => new NotFound()
        });
    }
}