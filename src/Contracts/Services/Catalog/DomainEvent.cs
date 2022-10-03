using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class DomainEvent
{
    public record CatalogCreated(Guid Id, string Title, string Description) : Message(CorrelationId: Id), IEvent;

    public record CatalogDeleted(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record CatalogActivated(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record CatalogDeactivated(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record CatalogTitleChanged(Guid Id, string Title) : Message(CorrelationId: Id), IEvent;

    public record CatalogDescriptionChanged(Guid Id, string Description) : Message(CorrelationId: Id), IEvent;

    public record CatalogItemAdded(Guid Id, Guid ItemId, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity) : Message(CorrelationId: Id), IEvent;

    public record CatalogItemRemoved(Guid Id, Guid ItemId) : Message(CorrelationId: Id), IEvent;

    public record CatalogItemIncreased(Guid Id, Guid ItemId, Guid InventoryId, int Quantity) : Message(CorrelationId: Id), IEvent;
}