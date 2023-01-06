using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Projection
{
    public record Inventory(Guid Id, Guid OwnerId, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.InventoryGridItem(Inventory inventory)
            => new()
            {
                InventoryId = inventory.Id.ToString(),
                OwnerId = inventory.OwnerId.ToString(),
                IsDeleted = inventory.IsDeleted
            };
    }

    public record InventoryItem(Guid Id, Guid InventoryId, Dto.Product Product, int Quantity, string Sku,
        bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.InventoryItemListItem(InventoryItem inventoryItem)
            => new()
            {
                Id = inventoryItem.Id.ToString(),
                InventoryId = inventoryItem.InventoryId.ToString(),
                IsDeleted = inventoryItem.IsDeleted,
                Product = inventoryItem.Product,
                Sku = inventoryItem.Sku,
                Quantity = inventoryItem.Quantity,
            };
    }
}