using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Catalog.Protobuf;

namespace Contracts.Services.Catalog;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.Catalog(Catalog catalog)
            => new()
            {
                Id = catalog.Id.ToString(),
                Title = catalog.Title,
                Description = catalog.Description,
                IsActive = catalog.IsActive,
                IsDeleted = catalog.IsDeleted
            };
    }

    public record CatalogItem
        (Guid CatalogId, Guid Id, Guid InventoryId, Dto.Product Product, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.CatalogItem(CatalogItem catalogItem)
            => new()
            {
                Id = catalogItem.Id.ToString(),
                CatalogId = catalogItem.CatalogId.ToString(),
                InventoryId = catalogItem.InventoryId.ToString(),
                Product = new Product
                {
                    Brand = catalogItem.Product.Brand,
                    Category = catalogItem.Product.Category,
                    Description = catalogItem.Product.Description,
                    Name = catalogItem.Product.Name,
                    PictureUrl = catalogItem.Product.PictureUrl,
                    Sku = catalogItem.Product.Sku,
                    Unit = catalogItem.Product.Unit
                },
                IsDeleted = catalogItem.IsDeleted
            };
    }
}