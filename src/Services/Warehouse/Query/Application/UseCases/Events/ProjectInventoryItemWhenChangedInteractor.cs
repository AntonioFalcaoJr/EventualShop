using Application.Abstractions;
using Contracts.Services.Warehouse;

namespace Application.UseCases.Events;

public interface IProjectInventoryItemWhenChangedInteractor :
    IInteractor<DomainEvent.InventoryAdjustmentDecreased>,
    IInteractor<DomainEvent.InventoryAdjustmentIncreased>,
    IInteractor<DomainEvent.InventoryItemIncreased>,
    IInteractor<DomainEvent.InventoryItemReceived> { }

public class ProjectInventoryItemWhenChangedInteractor : IProjectInventoryItemWhenChangedInteractor
{
    private readonly IProjectionGateway<Projection.InventoryItemListItem> _projectionGateway;

    public ProjectInventoryItemWhenChangedInteractor(IProjectionGateway<Projection.InventoryItemListItem> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }
    
    public async Task InteractAsync(DomainEvent.InventoryAdjustmentDecreased @event,  CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            field: item => item.Quantity,
            value: @event.Quantity * -1,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryAdjustmentIncreased @event,  CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            id: @event.ItemId,
            field: item => item.Quantity,
            value: @event.Quantity,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryItemIncreased @event,  CancellationToken cancellationToken)
        => await _projectionGateway.UpdateFieldAsync(
            @id: @event.ItemId,
            field: item => item.Quantity,
            @value: @event.Quantity,
            cancellationToken: cancellationToken);

    public async Task InteractAsync(DomainEvent.InventoryItemReceived @event,  CancellationToken cancellationToken)
    {
        Projection.InventoryItemListItem inventoryItemListItem = new(
            @event.ItemId,
            @event.InventoryId,
            @event.Product,
            @event.Quantity,
            @event.Sku,
            false);

         await _projectionGateway.InsertAsync(inventoryItemListItem, cancellationToken);
    }
}