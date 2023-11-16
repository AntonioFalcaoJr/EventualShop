using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Command = Contracts.Boundaries.Warehouse.Command;
using DomainEvent = Contracts.Boundaries.Shopping.Shopping.DomainEvent;

namespace Application.UseCases.Events;

public interface IReserveInventoryItemWhenCartItemAddedInteractor : IInteractor<DomainEvent.CartItemAdded> { }

public class ReserveInventoryItemWhenCartItemAddedInteractor(IApplicationService service) : IReserveInventoryItemWhenCartItemAddedInteractor
{
    public async Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
    {
        var inventory = await service.LoadAggregateAsync<Inventory>(Guid.NewGuid() /*@event.InventoryId*/, cancellationToken);

        inventory.Handle(new Command.ReserveInventoryItem(
            Guid.NewGuid(), // @event.InventoryId,
            Guid.NewGuid(), // TODO - solve relationship
            Guid.NewGuid(), // @event.CartId,
            @event.Product,
            @event.Quantity));

        await service.AppendEventsAsync(inventory, cancellationToken);
    }
}