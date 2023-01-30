using Domain.Abstractions.Aggregates;
using Contracts.Abstractions.Messages;
using Contracts.Services.Warehouse;
using Domain.Entities.Adjustments;
using Domain.Entities.InventoryItems;
using Domain.ValueObjects.Products;
using Newtonsoft.Json;

namespace Domain.Aggregates;

public class Inventory : AggregateRoot<InventoryValidator>
{
    [JsonProperty]
    private readonly List<InventoryItem> _items = new();

    private static DateTimeOffset Expiration => DateTimeOffset.Now.AddMinutes(5);

    public Guid OwnerId { get; private set; }

    public int TotalItems
        => Items.Count();

    public int TotalUnits
        => Items.Sum(item => item.Quantity);

    public IEnumerable<InventoryItem> Items
        => _items.AsReadOnly();

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    public void Handle(Command.CreateInventory cmd)
        => RaiseEvent<DomainEvent.InventoryCreated>(version => new(Guid.NewGuid(), cmd.OwnerId, version));

    public void Handle(Command.ReceiveInventoryItem cmd)
    {
        var item = _items
            .Where(inventoryItem => inventoryItem.Product == cmd.Product)
            .SingleOrDefault(inventoryItem => inventoryItem.Cost == cmd.Cost);

        RaiseEvent(version => item is { IsDeleted: false }
            ? new DomainEvent.InventoryItemIncreased(cmd.InventoryId, item.Id, cmd.Quantity, version)
            : new DomainEvent.InventoryItemReceived(cmd.InventoryId, Guid.NewGuid(), cmd.Product, cmd.Cost, cmd.Quantity, FormatSku(cmd.Product), version));
    }

    public void Handle(Command.DecreaseInventoryAdjust cmd)
    {
        if (_items.SingleOrDefault(inventoryItem => inventoryItem.Id == cmd.ItemId) is not { IsDeleted: false } item) return;

        RaiseEvent(version => item.QuantityAvailable >= cmd.Quantity
            ? new DomainEvent.InventoryAdjustmentDecreased(cmd.InventoryId, cmd.ItemId, cmd.Reason, cmd.Quantity, version)
            : new DomainEvent.InventoryAdjustmentNotDecreased(cmd.InventoryId, cmd.ItemId, cmd.Reason, cmd.Quantity, item.QuantityAvailable, version));
    }

    public void Handle(Command.IncreaseInventoryAdjust cmd)
    {
        if (_items.SingleOrDefault(item => item.Id == cmd.ItemId) is not { IsDeleted: false }) return;
        RaiseEvent<DomainEvent.InventoryAdjustmentIncreased>(version => new(cmd.InventoryId, cmd.ItemId, cmd.Reason, cmd.Quantity, version));
    }

    public void Handle(Command.ReserveInventoryItem cmd)
    {
        if (_items.SingleOrDefault(inventoryItem => inventoryItem.Product == cmd.Product) is not { IsDeleted: false } item) return;

        RaiseEvent(version => item.QuantityAvailable switch
        {
            < 1 => new DomainEvent.StockDepleted(cmd.InventoryId, item.Id, item.Product, version),
            var availability when availability >= cmd.Quantity => new DomainEvent.InventoryReserved(cmd.InventoryId, item.Id, cmd.CatalogId, cmd.CartId, cmd.Product, cmd.Quantity, Expiration, version),
            _ => new DomainEvent.InventoryNotReserved(cmd.InventoryId, item.Id, cmd.CartId, cmd.Quantity, item.QuantityAvailable, version)
        });
    }

    protected override void Apply(IDomainEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.InventoryCreated @event)
        => (Id, OwnerId, _) = @event;

    private void When(DomainEvent.InventoryItemReceived @event)
        => _items.Add(new(@event.ItemId, @event.Cost, @event.Product, @event.Quantity, @event.Sku));

    private void When(DomainEvent.InventoryItemIncreased @event)
        => _items
            .Single(item => item.Id == @event.ItemId)
            .Increase(@event.Quantity);

    private void When(DomainEvent.InventoryItemDecreased @event)
        => _items
            .Single(item => item.Id == @event.ItemId)
            .Decrease(@event.Quantity);

    private void When(DomainEvent.InventoryAdjustmentIncreased @event)
        => _items
            .Single(item => item.Id == @event.ItemId)
            .Adjust(new IncreaseAdjustment(@event.Reason, @event.Quantity));

    private void When(DomainEvent.InventoryAdjustmentDecreased @event)
        => _items
            .Single(item => item.Id == @event.ItemId)
            .Adjust(new DecreaseAdjustment(@event.Reason, @event.Quantity));

    private void When(DomainEvent.InventoryReserved @event)
        => _items
            .Single(item => item.Id == @event.ItemId)
            .Reserve(@event.Quantity, @event.CartId, @event.Expiration);

    private void When(DomainEvent.InventoryNotReserved _) { }

    private string FormatSku(Product product)
    {
        var count = _items
            .Where(item => item.Product.Brand == product.Brand)
            .Where(item => item.Product.Category == product.Category)
            .Count(item => item.Product.Unit == product.Unit);

        return $"{product.Brand[..2]}{product.Category[..2]}{product.Unit[..2]}{count + 1}".ToUpperInvariant();
    }
}