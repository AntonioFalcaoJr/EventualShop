using System;
using System.Collections.Generic;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Domain.Entities.CatalogItems;

namespace Domain.Entities.Catalogs
{
    public class Catalog : AggregateRoot<Guid>
    {
        private readonly List<CatalogItem> _items = new();
        public bool IsDeleted { get; private set; }
        public bool IsActive { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<CatalogItem> Items => _items;

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

        public void AddItem(Guid id, CatalogItem catalogItem)
            => RaiseEvent(new Events.CatalogItemAdded(id, catalogItem));

        public void RemoveItem(Guid id, Guid catalogItemId)
            => RaiseEvent(new Events.CatalogItemRemoved(id, catalogItemId));

        public void EditItem(Guid id)
            => RaiseEvent(new Events.CatalogItemEdited(id));

        protected override void ApplyEvent(IDomainEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.CatalogCreated @event)
            => (Id, Title) = @event;

        private void When(Events.CatalogDeleted _)
            => IsDeleted = true;

        private void When(Events.CatalogItemAdded @event)
        {
            if (@event.CatalogItem.IsValid is false)
            {
                AddError("Invalid Item", @event.CatalogItem.Errors);
                return;
            }

            if (_items.Exists(item => item.Id == @event.CatalogItem.Id)) return;

            _items.Add(@event.CatalogItem);
        }

        private void When(Events.CatalogItemRemoved @event)
            => _items.RemoveAll(item => item.Id == @event.CatalogItemId);

        private void When(Events.CatalogActivated _)
            => IsActive = true;

        private void When(Events.CatalogDeactivated _)
            => IsActive = false;

        protected sealed override bool Validate()
            => OnValidate<Validator, Catalog>();
    }
}