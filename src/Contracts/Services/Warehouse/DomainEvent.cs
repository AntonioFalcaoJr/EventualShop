using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class DomainEvent
{
    public record InventoryCreated(Guid Id, Guid OwnerId) : Message, IEvent;

    public record InventoryItemReceived(Guid Id, Guid ItemId, Dto.Product Product, decimal Cost, int Quantity, string Sku) : Message, IEvent;

    public record InventoryAdjustmentIncreased(Guid Id, Guid ItemId, string Reason, int Quantity) : Message, IEvent;

    public record InventoryAdjustmentDecreased(Guid Id, Guid ItemId, string Reason, int Quantity) : Message, IEvent;

    public record InventoryAdjustmentNotDecreased(Guid Id, Guid ItemId, string Reason, int QuantityDesired, int QuantityAvailable) : Message, IEvent;

    public record InventoryReserved(Guid Id, Guid ItemId, Guid CatalogId, Guid CartId, Dto.Product Product, int Quantity, DateTimeOffset Expiration) : Message, IEvent;

    public record StockDepleted(Guid Id, Guid ItemId, Dto.Product Product) : Message, IEvent;

    public record InventoryNotReserved(Guid Id, Guid ItemId, Guid CartId, int QuantityDesired, int QuantityAvailable) : Message, IEvent;

    public record InventoryItemIncreased(Guid Id, Guid ItemId, int Quantity) : Message, IEvent;

    public record InventoryItemDecreased(Guid Id, Guid ItemId, int Quantity) : Message, IEvent;
}