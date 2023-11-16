using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Cataloging.Catalog;

public static class DomainEvent
{
    public record CatalogCreated(string CatalogId, string AppId, string Title, string Description, string Version) : Message, IDomainEvent;

    public record CatalogDeleted(string CatalogId, string Status, string Version) : Message, IDomainEvent;

    public record CatalogActivated(string CatalogId, string Status, string Version) : Message, IDomainEvent;

    public record CatalogInactivated(string CatalogId, string Status, string Version) : Message, IDomainEvent;

    public record CatalogTitleChanged(Guid CatalogId, string Title, string Version) : Message, IDomainEvent;

    public record CatalogDescriptionChanged(Guid CatalogId, string Description, string Version) : Message, IDomainEvent;

    public record CatalogItemAdded(Guid CatalogId, Guid ItemId, Guid InventoryId, Dto.Product Product, Dto.Money UnitPrice, string Sku, int Quantity, string Version) : Message, IDomainEvent;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId, string Version) : Message, IDomainEvent;

    public record CatalogItemIncreased(Guid CatalogId, Guid ItemId, Guid InventoryId, int Quantity, string Version) : Message, IDomainEvent;
}