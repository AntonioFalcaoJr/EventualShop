using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class DomainEvent
{
    public record CatalogCreated(Guid CatalogId, string Title, string Description, long Version) : Message, IDomainEvent;

    public record CatalogDeleted(Guid CatalogId, long Version) : Message, IDomainEvent;

    public record CatalogActivated(Guid CatalogId, long Version) : Message, IDomainEvent;

    public record CatalogDeactivated(Guid CatalogId, long Version) : Message, IDomainEvent;

    public record CatalogTitleChanged(Guid CatalogId, string Title, long Version) : Message, IDomainEvent;

    public record CatalogDescriptionChanged(Guid CatalogId, string Description, long Version) : Message, IDomainEvent;

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, Guid InventoryId, Dto.Product Product, Dto.Money UnitPrice, string Sku, int Quantity, long Version) : Message, IDomainEvent;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId, long Version) : Message, IDomainEvent;

    public record CatalogItemIncreased(Guid CatalogId, Guid ItemId, Guid InventoryId, int Quantity, long Version) : Message, IDomainEvent;
}