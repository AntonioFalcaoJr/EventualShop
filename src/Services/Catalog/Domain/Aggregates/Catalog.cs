using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Catalogs;

namespace Domain.Aggregates;

public class Catalog : AggregateRoot<Guid, CatalogValidator>
{
    private readonly List<CatalogItem> _items = new();
    public bool IsActive { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public IEnumerable<CatalogItem> Items
        => _items;

    public void Handle(Commands.CreateCatalog cmd)
        => RaiseEvent(new DomainEvents.CatalogCreated(Guid.NewGuid(), cmd.Title, cmd.Description));

    public void Handle(Commands.DeleteCatalog cmd)
        => RaiseEvent(new DomainEvents.CatalogDeleted(cmd.CatalogId));

    public void Handle(Commands.ActivateCatalog cmd)
        => RaiseEvent(new DomainEvents.CatalogActivated(cmd.CatalogId));

    public void Handle(Commands.DeactivateCatalog cmd)
        => RaiseEvent(new DomainEvents.CatalogDeactivated(cmd.CatalogId));

    public void Handle(Commands.ChangeCatalogDescription cmd)
        => RaiseEvent(new DomainEvents.CatalogDescriptionChanged(cmd.CatalogId, cmd.Description));

    public void Handle(Commands.ChangeCatalogTitle cmd)
        => RaiseEvent(new DomainEvents.CatalogTitleChanged(cmd.CatalogId, cmd.Title));

    public void Handle(Commands.AddCatalogItem cmd)
        => RaiseEvent(new DomainEvents.CatalogItemAdded(cmd.CatalogId, Guid.NewGuid(), cmd.Name, cmd.Description, cmd.Price, cmd.PictureUri));

    public void Handle(Commands.DeleteCatalogItem cmd)
        => RaiseEvent(new DomainEvents.CatalogItemRemoved(cmd.CatalogId, cmd.CatalogItemId));

    public void Handle(Commands.UpdateCatalogItem cmd)
        => RaiseEvent(new DomainEvents.CatalogItemUpdated(cmd.CatalogId, cmd.CatalogItemId, cmd.Name, cmd.Description, cmd.Price, cmd.PictureUri));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.CatalogCreated @event)
        => (Id, Title, Description) = @event;

    private void When(DomainEvents.CatalogDescriptionChanged @event)
        => Description = @event.Description;
    
    private void When(DomainEvents.CatalogTitleChanged @event)
        => Title = @event.Title;

    private void When(DomainEvents.CatalogDeleted _)
        => IsDeleted = true;

    private void When(DomainEvents.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.ItemId);

    private void When(DomainEvents.CatalogActivated _)
        => IsActive = true;

    private void When(DomainEvents.CatalogDeactivated _)
        => IsActive = false;

    private void When(DomainEvents.CatalogItemAdded @event)
    {
        var catalogItem = new CatalogItem(
            @event.ItemId,
            @event.Name,
            @event.Description,
            @event.Price,
            @event.PictureUri);

        _items.Add(catalogItem);
    }

    private void When(DomainEvents.CatalogItemUpdated @event)
        => _items
            .Where(item => item.Id == @event.ItemId)
            .ToList()
            .ForEach(catalogItem =>
            {
                catalogItem.SetDescription(@event.Description);
                catalogItem.SetName(@event.Name);
                catalogItem.SetPrice(@event.Price);
                catalogItem.SetPictureUri(@event.PictureUri);
            });
}