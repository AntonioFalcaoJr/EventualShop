using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Warehouse;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetInventoryItemsConsumer : IConsumer<Query.GetInventoryItems>
{
    private readonly IProjectionRepository<Projection.InventoryItem> _repository;

    public GetInventoryItemsConsumer(IProjectionRepository<Projection.InventoryItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetInventoryItems> context)
    {
        var inventoryItems
            = await _repository.GetAllAsync(
                limit: context.Message.Limit,
                offset: context.Message.Offset,
                predicate: item => item.InventoryId == context.Message.InventoryId,
                cancellationToken: context.CancellationToken);

        await context.RespondAsync(inventoryItems switch
        {
            {Page.Size: > 0} => inventoryItems,
            {Page.Size: < 1} => new Reply.NoContent(),
            _ => new Reply.NotFound()
        });
    }
}