using Domain.Abstractions.Aggregates;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Warehouse;

namespace Domain.Aggregates;

public class Product : AggregateRoot<Guid>
{
    public string Sku { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int Quantity { get; private set; }

    public void Handle(Commands.ReceiveProduct cmd)
        => RaiseEvent(new DomainEvents.ProductReceived(Guid.NewGuid(), cmd.Sku, cmd.Name, cmd.Description, cmd.Quantity));

    public void Handle(Commands.AdjustInventory cmd)
        => RaiseEvent(new DomainEvents.InventoryAdjusted(cmd.ProductId, cmd.Sku, cmd.Quantity));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.ProductReceived @event)
        => (Id, Sku, Name, Description, Quantity) = @event;

    private void When(DomainEvents.InventoryAdjusted @event)
        => Quantity = @event.Quantity;

    protected sealed override bool Validate()
        => OnValidate<ProductValidator, Product>();
}