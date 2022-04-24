using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetInventoryItemDetailsConsumer : IConsumer<Query.GetInventoryItemDetails>
{
    private readonly IProjectionRepository<Projection.Inventory> _projectionRepository;

    public GetInventoryItemDetailsConsumer(IProjectionRepository<Projection.Inventory> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetInventoryItemDetails> context)
    {
        var inventory = await _projectionRepository.GetAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync(inventory);
    }
}