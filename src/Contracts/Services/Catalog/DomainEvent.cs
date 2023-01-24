using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class DomainEvent
{
    public record CatalogCreated(Guid CatalogId, string Title, string Description) : Message, IEvent;

    public record CatalogDeleted(Guid CatalogId) : Message, IEvent;

    public record CatalogActivated(Guid CatalogId) : Message, IEvent;

    public record CatalogDeactivated(Guid CatalogId) : Message, IEvent;

    public record CatalogTitleChanged(Guid CatalogId, string Title) : Message, IEvent;

    public record CatalogDescriptionChanged(Guid CatalogId, string Description) : Message, IEvent;

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, Guid InventoryId, Dto.Product Product, Dto.Money UnitPrice, string Sku, int Quantity) : Message, IEvent;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId) : Message, IEvent;

    public record CatalogItemIncreased(Guid CatalogId, Guid ItemId, Guid InventoryId, int Quantity) : Message, IEvent;
}