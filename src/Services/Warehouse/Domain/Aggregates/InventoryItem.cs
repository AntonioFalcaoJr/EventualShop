using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Warehouse;

namespace Domain.Aggregates;

public class InventoryItem : AggregateRoot<Guid>
{
    private readonly List<Reserve> _reserves = new();

    public string Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }

    public int QuantityAvailable
        => Quantity - QuantityReserved;

    public int QuantityReserved
        => _reserves.Sum(reserve => reserve.Quantity);

    public IEnumerable<Reserve> Reserves
        => _reserves;

    public void Handle(Commands.ReceiveInventoryItem cmd)
        => RaiseEvent(new DomainEvents.InventoryItemReceived(Guid.NewGuid(), cmd.Sku, cmd.Name, cmd.Description, cmd.Quantity));

    public void Handle(Commands.AdjustInventory cmd)
        => RaiseEvent(new DomainEvents.InventoryAdjusted(cmd.ProductId, cmd.Sku, cmd.Quantity));

    public void Handle(Commands.ReserveInventory cmd)
        => RaiseEvent(cmd.Quantity switch
        {
            _ when QuantityAvailable <= 0
                => new DomainEvents.StockDepleted(cmd.ProductId, cmd.Sku),
            var quantityDesired when quantityDesired <= QuantityAvailable
                => new DomainEvents.InventoryReserved(cmd.ProductId, cmd.CartId, cmd.Sku, cmd.Quantity),
            var quantityDesired when quantityDesired > QuantityAvailable
                => new DomainEvents.InventoryNotReserved(cmd.ProductId, cmd.CartId, cmd.Sku, quantityDesired, QuantityAvailable),
            _ => default
        });

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.InventoryItemReceived @event)
        => (Id, Sku, Name, Description, Quantity) = @event;

    private void When(DomainEvents.InventoryAdjusted @event)
        => Quantity = @event.Quantity;

    private void When(DomainEvents.StockDepleted _)
    {
        // Status = OutOfStock;
    }

    private void When(DomainEvents.InventoryReserved @event)
        => _reserves.Add(new()
        {
            Quantity = @event.Quantity,
            CartId = @event.OrderId,
            ProductId = @event.ProductId
        });

    private void When(DomainEvents.InventoryNotReserved _) { }

    protected sealed override bool Validate()
        => OnValidate<InventoryItemValidator, InventoryItem>();
}