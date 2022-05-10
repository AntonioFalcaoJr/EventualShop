using Domain.Abstractions.Aggregates;
using Domain.Entities;
using Domain.Entities.Products;
using Contracts.Abstractions;
using Contracts.Services.Warehouse;

namespace Domain.Aggregates;

public class InventoryItem : AggregateRoot<Guid, InventoryItemValidator>
{
    private readonly List<Reserve> _reserves = new();

    public Product Product { get; private set; }
    public int Quantity { get; private set; }

    public int QuantityAvailable
        => Quantity - QuantityReserved;

    public int QuantityReserved
        => _reserves.Sum(reserve => reserve.Quantity);

    public IEnumerable<Reserve> Reserves
        => _reserves;

    public void Handle(Command.ReceiveInventoryItem cmd)
        => RaiseEvent(new DomainEvent.InventoryReceived(Guid.NewGuid(), cmd.Product, cmd.Quantity));

    public void Handle(Command.DecreaseInventoryAdjust cmd)
        => RaiseEvent(new DomainEvent.InventoryAdjustmentDecreased(cmd.ProductId, cmd.Reason, cmd.Quantity));

    public void Handle(Command.IncreaseInventoryAdjust cmd)
        => RaiseEvent(new DomainEvent.InventoryAdjustmentIncreased(cmd.ProductId, cmd.Reason, cmd.Quantity));

    public void Handle(Command.ReserveInventory cmd)
        => RaiseEvent(cmd.Quantity switch
        {
            _ when QuantityAvailable <= 0
                => new DomainEvent.StockDepleted(cmd.ProductId, cmd.Sku),
            var quantityDesired when quantityDesired <= QuantityAvailable
                => new DomainEvent.InventoryReserved(cmd.ProductId, cmd.CartId, cmd.Sku, cmd.Quantity),
            var quantityDesired when quantityDesired > QuantityAvailable
                => new DomainEvent.InventoryNotReserved(cmd.ProductId, cmd.CartId, cmd.Sku, quantityDesired, QuantityAvailable),
            _ => default
        });

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.InventoryReceived @event) 
        => (Id, Product, Quantity) = @event;

    private void When(DomainEvent.InventoryAdjustmentDecreased @event)
        => Quantity -= @event.Quantity;

    private void When(DomainEvent.InventoryAdjustmentIncreased @event)
        => Quantity += @event.Quantity;

    private void When(DomainEvent.StockDepleted _)
    {
        // Status = OutOfStock;
    }

    private void When(DomainEvent.InventoryReserved @event)
        => _reserves.Add(new()
        {
            Quantity = @event.Quantity,
            CartId = @event.OrderId,
            ProductId = @event.ProductId
        });

    private void When(DomainEvent.InventoryNotReserved _) { }
}