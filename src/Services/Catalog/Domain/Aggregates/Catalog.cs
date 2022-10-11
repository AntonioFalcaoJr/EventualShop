using System.Collections.ObjectModel;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog;

namespace Domain.Aggregates;

public class Catalog : AggregateRoot<Guid, CatalogValidator>
{
    private readonly List<CatalogItem> _items = new();
    public bool IsActive { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public IEnumerable<CatalogItem> Items
        => _items;

    public void Handle(Command.CreateCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogCreated(cmd.Id, cmd.Title, cmd.Description));

    public void Handle(Command.DeleteCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogDeleted(cmd.Id));

    public void Handle(Command.ActivateCatalog cmd)
    {
        if (Items.Any() && IsActive is false)
            RaiseEvent(new DomainEvent.CatalogActivated(cmd.Id));
    }

    public void Handle(Command.DeactivateCatalog cmd)
    {
        if (IsActive)
            RaiseEvent(new DomainEvent.CatalogDeactivated(cmd.Id));
    }

    public void Handle(Command.ChangeCatalogDescription cmd)
        => RaiseEvent(new DomainEvent.CatalogDescriptionChanged(cmd.Id, cmd.Description));

    public void Handle(Command.ChangeCatalogTitle cmd)
        => RaiseEvent(new DomainEvent.CatalogTitleChanged(cmd.Id, cmd.Title));

    public void Handle(Command.AddCatalogItem cmd)
        => RaiseEvent(_items
            .Where(catalogItem => catalogItem.Product == cmd.Product)
            .SingleOrDefault(catalogItem => catalogItem.UnitPrice == cmd.UnitPrice) is {IsDeleted: false} item
            ? new DomainEvent.CatalogItemIncreased(cmd.Id, item.Id, cmd.InventoryId, cmd.Quantity)
            : new DomainEvent.CatalogItemAdded(cmd.Id, Guid.NewGuid(), cmd.InventoryId, cmd.Product, cmd.UnitPrice, cmd.Sku, cmd.Quantity));

    public void Handle(Command.RemoveCatalogItem cmd)
        => RaiseEvent(new DomainEvent.CatalogItemRemoved(cmd.Id, cmd.ItemId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CatalogCreated @event)
        => (Id, Title, Description) = @event;

    private void When(DomainEvent.CatalogDescriptionChanged @event)
        => Description = @event.Description;

    private void When(DomainEvent.CatalogTitleChanged @event)
        => Title = @event.Title;

    private void When(DomainEvent.CatalogDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.CatalogActivated _)
        => IsActive = true;

    private void When(DomainEvent.CatalogDeactivated _)
        => IsActive = false;

    private void When(DomainEvent.CatalogItemAdded @event)
        => _items.Add(new(@event.ItemId, @event.InventoryId, @event.Product, @event.UnitPrice, @event.Sku, @event.Quantity));

    private void When(DomainEvent.CatalogItemIncreased @event)
        => _items.Single(item => item.Id == @event.ItemId).Increase(@event.Quantity);

    private void When(DomainEvent.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);
}