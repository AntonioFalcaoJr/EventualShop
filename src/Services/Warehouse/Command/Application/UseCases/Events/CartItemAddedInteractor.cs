using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Command = Contracts.Services.Warehouse.Command;
using DomainEvent = Contracts.Services.ShoppingCart.DomainEvent;

namespace Application.UseCases.Events;

public interface ICartItemAddedInteractor : IInteractor<DomainEvent.CartItemAdded> { }

public class CartItemAddedInteractor : ICartItemAddedInteractor
{
    private readonly IApplicationService _applicationService;

    public CartItemAddedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.CartItemAdded @event, CancellationToken cancellationToken)
    {
        var inventory = await _applicationService.LoadAggregateAsync<Inventory>(@event.InventoryId, cancellationToken);

        inventory.Handle(new Command.ReserveInventoryItem(
            @event.InventoryId,
            Guid.NewGuid(), // TODO - solve relationship
            @event.CartId,
            @event.Product,
            @event.Quantity));

        await _applicationService.AppendEventsAsync(inventory, cancellationToken);
    }
}