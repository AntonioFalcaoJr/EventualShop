using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Catalog;

namespace Domain.Aggregates;

public class Catalog : AggregateRoot<Guid>
{
    private readonly List<CatalogItem> _items = new();
    public bool IsActive { get; private set; }
    public string Title { get; private set; }

    public IEnumerable<CatalogItem> Items
        => _items;

    public void Create(string title)
        => RaiseEvent(new DomainEvents.CatalogCreated(Guid.NewGuid(), title));

    public void Delete(Guid id)
        => RaiseEvent(new DomainEvents.CatalogDeleted(id));

    public void Activate(Guid id)
        => RaiseEvent(new DomainEvents.CatalogActivated(id));

    public void Deactivate(Guid id)
        => RaiseEvent(new DomainEvents.CatalogDeactivated(id));

    public void Update(Guid id, string title)
        => RaiseEvent(new DomainEvents.CatalogUpdated(id, title));

    public void AddItem(Guid id, string name, string description, decimal price, string pictureUri)
        => RaiseEvent(new DomainEvents.CatalogItemAdded(id, name, description, price, pictureUri));

    public void RemoveItem(Guid id, Guid itemId)
        => RaiseEvent(new DomainEvents.CatalogItemRemoved(id, itemId));

    public void UpdateItem(Guid id, Guid catalogItemId, string name, string description, decimal price, string pictureUri)
        => RaiseEvent(new DomainEvents.CatalogItemUpdated(id, catalogItemId, name, description, price, pictureUri));

    protected override void ApplyEvent(IEvent @event)
        => When(@event as dynamic);

    private void When(DomainEvents.CatalogCreated @event)
        => (Id, Title) = @event;

    private void When(DomainEvents.CatalogUpdated @event)
        => Title = @event.Title;

    private void When(DomainEvents.CatalogDeleted _)
        => IsDeleted = true;

    private void When(DomainEvents.CatalogItemRemoved @event)
        => _items.RemoveAll(item => item.Id == @event.CatalogItemId);

    private void When(DomainEvents.CatalogActivated _)
        => IsActive = true;

    private void When(DomainEvents.CatalogDeactivated _)
        => IsActive = false;

    private void When(DomainEvents.CatalogItemAdded @event)
    {
        var catalogItem = new CatalogItem(
            @event.Name,
            @event.Description,
            @event.Price,
            @event.PictureUri);

        _items.Add(catalogItem);
    }

    private void When(DomainEvents.CatalogItemUpdated @event)
        => _items
            .Where(item => item.Id == @event.CatalogItemId)
            .ToList()
            .ForEach(catalogItem =>
            {
                catalogItem.SetDescription(@event.Description);
                catalogItem.SetName(@event.Name);
                catalogItem.SetPrice(@event.Price);
                catalogItem.SetPictureUri(@event.PictureUri);
            });

    protected sealed override bool Validate()
        => OnValidate<CatalogValidator, Catalog>();
}