using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Cataloging.CatalogItem;

public static class DomainEvent
{
    public record CatalogItemCreated(string CatalogId, string AppId, string ItemId, string ProductId, string Version) : Message, IDomainEvent;

    public record CatalogItemRemoved(Guid CatalogId, Guid ItemId, string Version) : Message, IDomainEvent;

    public record CatalogItemIncreased(Guid CatalogId, Guid ItemId, Guid InventoryId, int Quantity, string Version) : Message, IDomainEvent;
}