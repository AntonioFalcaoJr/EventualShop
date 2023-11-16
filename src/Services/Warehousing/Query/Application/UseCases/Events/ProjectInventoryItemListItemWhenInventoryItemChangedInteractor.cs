using Application.Abstractions;
using Contracts.Boundaries.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryItemListItemWhenInventoryItemChangedInteractor :
    IInteractor<DomainEvent.InventoryAdjustmentDecreased>,
    IInteractor<DomainEvent.InventoryAdjustmentIncreased>,
    IInteractor<DomainEvent.InventoryItemIncreased>,
    IInteractor<DomainEvent.InventoryItemReceived> { }

public class ProjectInventoryItemListItemWhenInventoryItemChangedInteractor(IProjectionGateway<Projection.InventoryItemListItem> projectionGateway)
    : IProjectInventoryItemListItemWhenInventoryItemChangedInteractor
{
    public async Task InteractAsync(DomainEvent.InventoryAdjustmentDecreased @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            version: @event.Version,
            field: item => item.Quantity,
            value: @event.Quantity * -1, // TODO: This is a hack, should be fixed in the domain event
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryAdjustmentIncreased @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            version: @event.Version,
            field: item => item.Quantity,
            value: @event.Quantity,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryItemIncreased @event, CancellationToken cancellationToken)
        => await projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            version: @event.Version,
            field: item => item.Quantity,
            value: @event.Quantity,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryItemReceived @event, CancellationToken cancellationToken)
    {
        Projection.InventoryItemListItem inventoryItemListItem = new(
            @event.ItemId,
            @event.InventoryId,
            @event.Product,
            @event.Quantity,
            @event.Sku,
            false,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(inventoryItemListItem, cancellationToken);
    }
}