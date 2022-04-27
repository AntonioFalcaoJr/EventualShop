using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using Domain.Entities.Products;
using Contracts.Abstractions;
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
        => RaiseEvent(new DomainEvent.CatalogCreated(cmd.CatalogId, cmd.Title, cmd.Description, false, false));

    public void Handle(Command.DeleteCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogDeleted(cmd.CatalogId));

    public void Handle(Command.ActivateCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogActivated(cmd.CatalogId));

    public void Handle(Command.DeactivateCatalog cmd)
        => RaiseEvent(new DomainEvent.CatalogDeactivated(cmd.CatalogId));

    public void Handle(Command.ChangeCatalogDescription cmd)
        => RaiseEvent(new DomainEvent.CatalogDescriptionChanged(cmd.CatalogId, cmd.Description));

    public void Handle(Command.ChangeCatalogTitle cmd)
        => RaiseEvent(new DomainEvent.CatalogTitleChanged(cmd.CatalogId, cmd.Title));

    public void Handle(Command.AddCatalogItem cmd)
        => RaiseEvent(new DomainEvent.CatalogItemAdded(cmd.CatalogId, Guid.NewGuid(), cmd.Product, cmd.Quantity));

    public void Handle(Command.DeleteCatalogItem cmd)
        => RaiseEvent(new DomainEvent.CatalogItemRemoved(cmd.CatalogId, cmd.CatalogItemId));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvent.CatalogCreated @event)
        => (Id, Title, Description, IsActive, IsDeleted) = @event;

    private void When(DomainEvent.CatalogDescriptionChanged @event)
        => Description = @event.Description;

    private void When(DomainEvent.CatalogTitleChanged @event)
        => Title = @event.Title;

    private void When(DomainEvent.CatalogDeleted _)
        => IsDeleted = true;

    private void When(DomainEvent.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);

    private void When(DomainEvent.CatalogActivated _)
        => IsActive = true;

    private void When(DomainEvent.CatalogDeactivated _)
        => IsActive = false;

    private void When(DomainEvent.CatalogItemAdded @event)
    {
        Product product = new(@event.Product.Id, @event.Product.Name, @event.Product.UnitPrice, @event.Product.PictureUrl, @event.Product.Sku);
        CatalogItem item = new(@event.ItemId, product, @event.Quantity);
        _items.Add(item);
    }
}