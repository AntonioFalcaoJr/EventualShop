using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Projection
{
    public record InventoryGridItem(Guid Id, Guid OwnerId, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.InventoryGridItem(InventoryGridItem inventoryGridItem)
            => new()
            {
                InventoryId = inventoryGridItem.Id.ToString(),
                OwnerId = inventoryGridItem.OwnerId.ToString(),
            };
    }

    public record InventoryItemListItem(Guid Id, Guid InventoryId, Dto.Product Product, int Quantity, string Sku,
        bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.InventoryItemListItem(InventoryItemListItem inventoryItemListItem)
            => new()
            {
                Id = inventoryItemListItem.Id.ToString(),
                InventoryId = inventoryItemListItem.InventoryId.ToString(),
                IsDeleted = inventoryItemListItem.IsDeleted,
                Product = inventoryItemListItem.Product,
                Sku = inventoryItemListItem.Sku,
                Quantity = inventoryItemListItem.Quantity,
            };
    }
}