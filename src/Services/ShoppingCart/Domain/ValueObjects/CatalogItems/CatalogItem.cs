using Contracts.DataTransferObjects;
using Domain.Abstractions.ValueObjects;
using Domain.ValueObjects.Products;

namespace Domain.ValueObjects.CatalogItems;

public record CatalogItem(Guid Id, Guid CatalogId, Guid InventoryId, Product Product, string Sku, int Quantity, decimal UnitPrice)
    : ValueObject<CatalogItemValidator>
{
    public static implicit operator CatalogItem(Dto.CatalogItem item)
        => new(item.Id, item.CatalogId, item.InventoryId, item.Product, item.Sku, item.Quantity, item.UnitPrice);

    public static implicit operator Dto.CatalogItem(CatalogItem item)
        => new(item.Id, item.CatalogId, item.InventoryId, item.Product, item.Sku, item.Quantity, item.UnitPrice);
}