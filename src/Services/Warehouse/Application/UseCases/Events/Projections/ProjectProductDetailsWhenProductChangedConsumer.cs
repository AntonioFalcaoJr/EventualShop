using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using InventoryAdjustedEvent = ECommerce.Contracts.Warehouse.DomainEvents.InventoryAdjusted;
using ProductReceivedEvent = ECommerce.Contracts.Warehouse.DomainEvents.ProductReceived;

namespace Application.UseCases.Events.Projections;

public class ProjectProductDetailsWhenProductChangedConsumer :
    IConsumer<InventoryAdjustedEvent>,
    IConsumer<ProductReceivedEvent>
{
    private readonly IWarehouseEventStoreService _eventStoreService;
    private readonly IWarehouseProjectionsService _projectionsService;

    public ProjectProductDetailsWhenProductChangedConsumer(
        IWarehouseEventStoreService eventStoreService,
        IWarehouseProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<InventoryAdjustedEvent> context)
        => ProjectAsync(context.Message.ProductId, context.CancellationToken);

    public Task Consume(ConsumeContext<ProductReceivedEvent> context)
        => ProjectAsync(context.Message.ProductId, context.CancellationToken);

    private async Task ProjectAsync(Guid productId, CancellationToken cancellationToken)
    {
        var product = await _eventStoreService.LoadAggregateFromStreamAsync(productId, cancellationToken);

        var productDetails = new ProductDetailsProjection
        {
            Id = product.Id,
            Description = product.Description,
            Name = product.Name,
            Quantity = product.Quantity,
            Sku = product.Sku,
            IsDeleted = product.IsDeleted
        };

        await _projectionsService.ProjectAsync(productDetails, cancellationToken);
    }
}