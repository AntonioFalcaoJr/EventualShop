using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;

namespace Domain.Aggregates;

public class Catalog : AggregateRoot<CatalogValidator>
{
    private readonly List<CatalogItem> _items = new();
    public bool IsActive { get; private set; }
    public string? Title { get; private set; }
    public string? Description { get; private set; }

    public IEnumerable<CatalogItem> Items
        => _items;

    public override void Handle(ICommand command)
        => Handle(command as dynamic);

    public void Handle(Command.CreateCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogCreated(cmd.CatalogId, cmd.Title, cmd.Description));

    private void Handle(Command.DeleteCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogDeleted(cmd.CatalogId));

    private void Handle(Command.ActivateCatalog cmd)
    {
        if (Items.Any() && IsActive is false)
            RaiseEvent(new DomainEvent.CatalogActivated(cmd.CatalogId));
    }

    private void Handle(Command.DeactivateCatalog cmd)
    {
        if (IsActive)
            RaiseEvent(new DomainEvent.CatalogDeactivated(cmd.CatalogId));
    }

    private void Handle(Command.ChangeCatalogDescription cmd)
        => RaiseEvent(new DomainEvent.CatalogDescriptionChanged(cmd.CatalogId, cmd.Description));

    private void Handle(Command.ChangeCatalogTitle cmd)
        => RaiseEvent(new DomainEvent.CatalogTitleChanged(cmd.CatalogId, cmd.Title));

    private void Handle(Command.AddCatalogItem cmd)
        => RaiseEvent(_items
            .Where(catalogItem => catalogItem.Product == cmd.Product)
            .SingleOrDefault(catalogItem => catalogItem.UnitPrice == cmd.UnitPrice) is { IsDeleted: false } item
            ? new DomainEvent.CatalogItemIncreased(cmd.CatalogId, item.Id, cmd.InventoryId, cmd.Quantity)
            : new DomainEvent.CatalogItemAdded(cmd.CatalogId, Guid.NewGuid(), cmd.InventoryId, cmd.Product, cmd.UnitPrice, cmd.Sku, cmd.Quantity));

    private void Handle(Command.RemoveCatalogItem cmd)
        => RaiseEvent(new DomainEvent.CatalogItemRemoved(cmd.CatalogId, cmd.ItemId));

    protected override void Apply(IEvent @event)
        => Apply(@event as dynamic);

    private void Apply(DomainEvent.CatalogCreated @event)
        => (Id, Title, Description) = @event;

    private void Apply(DomainEvent.CatalogDescriptionChanged @event)
        => Description = @event.Description;

    private void Apply(DomainEvent.CatalogTitleChanged @event)
        => Title = @event.Title;

    private void Apply(DomainEvent.CatalogDeleted _)
        => IsDeleted = true;

    private void Apply(DomainEvent.CatalogActivated _)
        => IsActive = true;

    private void Apply(DomainEvent.CatalogDeactivated _)
        => IsActive = false;

    private void Apply(DomainEvent.CatalogItemAdded @event)
        => _items.Add(new(@event.ItemId, @event.InventoryId, @event.Product, @event.UnitPrice, @event.Sku, @event.Quantity));

    private void Apply(DomainEvent.CatalogItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase(@event.Quantity);

    private void Apply(DomainEvent.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);
}