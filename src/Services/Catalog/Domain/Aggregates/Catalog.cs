using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Entities.CatalogItems;
using Messages.Abstractions.Events;
using Messages.Catalogs;

namespace Domain.Aggregates
{
    public class Catalog : AggregateRoot<Guid>
    {
        private readonly List<CatalogItem> _items = new();
        public bool IsActive { get; private set; }
        public string Title { get; private set; }

        public IEnumerable<CatalogItem> Items
            => _items;

        public void Create(string title)
            => RaiseEvent(new Events.CatalogCreated(Guid.NewGuid(), title));

        public void Delete(Guid id)
            => RaiseEvent(new Events.CatalogDeleted(id));

        public void Activate(Guid id)
            => RaiseEvent(new Events.CatalogActivated(id));

        public void Deactivate(Guid id)
            => RaiseEvent(new Events.CatalogDeactivated(id));

        public void Update(Guid id, string title)
            => RaiseEvent(new Events.CatalogUpdated(id, title));

        public void AddItem(Guid id, string name, string description, decimal price, string pictureUri)
            => RaiseEvent(new Events.CatalogItemAdded(id, name, description, price, pictureUri));

        public void RemoveItem(Guid id, Guid itemId)
            => RaiseEvent(new Events.CatalogItemRemoved(id, itemId));

        public void UpdateItem(Guid id, Guid catalogItemId, string name, string description, decimal price, string pictureUri)
            => RaiseEvent(new Events.CatalogItemUpdated(id, catalogItemId, name, description, price, pictureUri));

        protected override void ApplyEvent(IEvent @event)
            => When(@event as dynamic);

        private void When(Events.CatalogCreated @event)
            => (Id, Title) = @event;

        private void When(Events.CatalogUpdated @event)
            => Title = @event.Title;

        private void When(Events.CatalogDeleted _)
            => IsDeleted = true;

        private void When(Events.CatalogItemRemoved @event)
            => _items.RemoveAll(item => item.Id == @event.CatalogItemId);

        private void When(Events.CatalogActivated _)
            => IsActive = true;

        private void When(Events.CatalogDeactivated _)
            => IsActive = false;

        private void When(Events.CatalogItemAdded @event)
        {
            var catalogItem = new CatalogItem(
                @event.Name,
                @event.Description,
                @event.Price,
                @event.PictureUri);

            _items.Add(catalogItem);
        }

        private void When(Events.CatalogItemUpdated @event)
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
}