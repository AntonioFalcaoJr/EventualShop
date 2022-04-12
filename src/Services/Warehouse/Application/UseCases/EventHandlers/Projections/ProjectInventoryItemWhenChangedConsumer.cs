using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Warehouses;
using MassTransit;
namespace Application.UseCases.EventHandlers.Projections;

public class ProjectInventoryItemWhenChangedConsumer :
    IConsumer<DomainEvents.InventoryAdjusted>,
    IConsumer<DomainEvents.InventoryItemReceived>
{
    private readonly IWarehouseEventStoreService _eventStoreService;
    private readonly IWarehouseProjectionsService _projectionsService;

    public ProjectInventoryItemWhenChangedConsumer(
        IWarehouseEventStoreService eventStoreService,
        IWarehouseProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.InventoryAdjusted> context)
        => ProjectAsync(context.Message.ProductId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.InventoryItemReceived> context)
        => ProjectAsync(context.Message.ProductId, context.CancellationToken);

    private async Task ProjectAsync(Guid productId, CancellationToken cancellationToken)
    {
        var inventoryItem = await _eventStoreService.LoadAggregateFromStreamAsync(productId, cancellationToken);

        var inventoryItemDetails = new InventoryItemDetailsProjection
        {
            Id = inventoryItem.Id,
            Description = inventoryItem.Description,
            Name = inventoryItem.Name,
            Quantity = inventoryItem.Quantity,
            Sku = inventoryItem.Sku,
            IsDeleted = inventoryItem.IsDeleted
        };

        await _projectionsService.ProjectAsync(inventoryItemDetails, cancellationToken);
    }
}