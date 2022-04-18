using ECommerce.Abstractions.Messages.Events;

namespace ECommerce.Contracts.Catalogs;

public static class DomainEvents
{
    public record CatalogCreated(Guid CatalogId, string Title, string Description, bool IsActive, bool IsDeleted) : Event(CorrelationId: CatalogId);

    public record CatalogDeleted(Guid CatalogId) : Event(CorrelationId: CatalogId);

    public record CatalogActivated(Guid CatalogId) : Event(CorrelationId: CatalogId);

    public record CatalogDeactivated(Guid CatalogId) : Event(CorrelationId: CatalogId);

    public record CatalogTitleChanged(Guid CatalogId, string Title) : Event(CorrelationId: CatalogId);

    public record CatalogDescriptionChanged(Guid CatalogId, string Description) : Event(CorrelationId: CatalogId);

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Event(CorrelationId: CatalogId);

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId) : Event(CorrelationId: CatalogId);

    public record CatalogItemUpdated(Guid CatalogId, Guid ItemId, string Name, string Description, decimal Price, string PictureUri) : Event(CorrelationId: CatalogId);
}