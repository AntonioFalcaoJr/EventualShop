using Application.Abstractions.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetInventoryItemDetailsConsumer : IConsumer<Queries.GetInventoryItemDetails>
{
    private readonly IProjectionRepository<Projections.InventoryProjection> _projectionRepository;

    public GetInventoryItemDetailsConsumer(IProjectionRepository<Projections.InventoryProjection> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetInventoryItemDetails> context)
    {
        var inventory = await _projectionRepository.GetAsync(context.Message.ProductId, context.CancellationToken);
        await context.RespondAsync(inventory);
    }
}