using System;
using Domain.Abstractions.Events;
using Domain.Entities.CatalogItems;

namespace Domain.Entities.Catalogs
{
    public static class Events
    {
        public record CatalogCreated(Guid Id, string Title) : DomainEvent;

        public record CatalogDeleted(Guid Id) : DomainEvent;

        public record CatalogActivated(Guid Id) : DomainEvent;

        public record CatalogDeactivated(Guid Id) : DomainEvent;

        public record CatalogUpdated(Guid Id, string Title) : DomainEvent;

        public record CatalogItemAdded(Guid Id, CatalogItem CatalogItem) : DomainEvent;

        public record CatalogItemRemoved(Guid Id, Guid CatalogItemId) : DomainEvent;

        public record CatalogItemUpdated(Guid Id, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : DomainEvent;
    }
}