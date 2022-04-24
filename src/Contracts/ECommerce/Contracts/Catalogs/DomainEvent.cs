using ECommerce.Abstractions;

namespace ECommerce.Contracts.Catalogs;

public static class DomainEvent
{
    public record CatalogCreated(Guid CatalogId, string Title, string Description, bool IsActive, bool IsDeleted) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogDeleted(Guid CatalogId) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogActivated(Guid CatalogId) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogDeactivated(Guid CatalogId) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogTitleChanged(Guid CatalogId, string Title) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogDescriptionChanged(Guid CatalogId, string Description) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId) : Message(CorrelationId: CatalogId), IEvent;

    public record CatalogItemUpdated(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Message(CorrelationId: CatalogId), IEvent;
}