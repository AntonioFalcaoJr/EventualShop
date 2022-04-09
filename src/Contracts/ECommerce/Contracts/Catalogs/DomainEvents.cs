using ECommerce.Abstractions.Messages.Events;

namespace ECommerce.Contracts.Catalogs;

public static class DomainEvents
{
    public record CatalogCreated(Guid CatalogId, string Title, string Description) : Event;

    public record CatalogDeleted(Guid CatalogId) : Event;

    public record CatalogActivated(Guid CatalogId) : Event;

    public record CatalogDeactivated(Guid CatalogId) : Event;

    public record CatalogUpdated(Guid CatalogId, string Title, string Description) : Event;

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Event;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId) : Event;

    public record CatalogItemUpdated(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Event;
}