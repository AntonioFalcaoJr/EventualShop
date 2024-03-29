﻿using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Projection
{
    public record InventoryGridItem(Guid Id, Guid OwnerId, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.InventoryGridItem(InventoryGridItem inventoryGridItem)
            => new()
            {
                InventoryId = inventoryGridItem.Id.ToString(),
                OwnerId = inventoryGridItem.OwnerId.ToString()
            };
    }

    public record InventoryItemListItem(Guid Id, Guid InventoryId, Dto.Product Product, int Quantity, string Sku, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.InventoryItemListItem(InventoryItemListItem item)
            => new()
            {
                ItemId = item.Id.ToString(),
                InventoryId = item.InventoryId.ToString(),
                Product = item.Product,
                Sku = item.Sku,
                Quantity = item.Quantity
            };
    }
}