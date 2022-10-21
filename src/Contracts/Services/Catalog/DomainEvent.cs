using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class DomainEvent
{
    public record CatalogCreated(Guid Id, string Title, string Description) : Message, IEvent;

    public record CatalogDeleted(Guid Id) : Message, IEvent;

    public record CatalogActivated(Guid Id) : Message, IEvent;

    public record CatalogDeactivated(Guid Id) : Message, IEvent;

    public record CatalogTitleChanged(Guid Id, string Title) : Message, IEvent;

    public record CatalogDescriptionChanged(Guid Id, string Description) : Message, IEvent;

    public record CatalogItemAdded(Guid Id, Guid ItemId, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity) : Message, IEvent;

    public record CatalogItemRemoved(Guid Id, Guid ItemId) : Message, IEvent;

    public record CatalogItemIncreased(Guid Id, Guid ItemId, Guid InventoryId, int Quantity) : Message, IEvent;
}