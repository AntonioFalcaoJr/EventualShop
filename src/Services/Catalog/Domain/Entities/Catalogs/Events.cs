using System;
using Domain.Abstractions.Events;
using Domain.Entities.CatalogItems;

namespace Domain.Entities.Catalogs
{
    public static class Events
    {
        public record CatalogCreated(Guid Id, string Title) : DomainEvent;

        public record CatalogItemAdded(Guid CatalogId, CatalogItem CatalogItem) : DomainEvent;

        public record CatalogItemRemoved(Guid CatalogId, Guid CatalogItemId) : DomainEvent;

        public record CatalogItemEdited(Guid Id) : DomainEvent;

        public record CatalogDeleted(Guid Id) : DomainEvent;

        public record CatalogActivated(Guid Id) : DomainEvent;

        public record CatalogDeactivated(Guid Id) : DomainEvent;

        public record CatalogUpdated(Guid Id, string Title) : DomainEvent;
    }
}