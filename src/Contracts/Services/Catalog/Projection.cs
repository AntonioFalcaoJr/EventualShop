using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Projection
{
    public record CatalogGridItem(Guid Id, string Title, string Description, string ImageUrl, bool IsActive, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.CatalogGridItem(CatalogGridItem catalog)
            => new()
            {
                CatalogId = catalog.Id.ToString(),
                Title = catalog.Title,
                Description = catalog.Description,
                ImageUrl = catalog.ImageUrl,
                IsActive = catalog.IsActive
            };
    }

    public record CatalogItemListItem(Guid Id, Guid CatalogId, Dto.Product Product, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.CatalogItemListItem(CatalogItemListItem item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                ProductName = item.Product.Name
            };
    }

    public record CatalogItemCard(Guid Id, Guid CatalogId, Dto.Product Product, Dto.Money Price, string ImageUrl, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.CatalogItemCard(CatalogItemCard item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                Product = item.Product,
                Description = item.Product.Description,
                ImageUrl = item.ImageUrl,
                UnitPrice = item.Price
            };
    }

    public record CatalogItemDetails(Guid Id, Guid CatalogId, Dto.Product Product, Dto.Money Price, string ImageUrl, bool IsDeleted, long Version) : IProjection
    {
        public static implicit operator Protobuf.CatalogItemDetails(CatalogItemDetails item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                Product = item.Product,
                Description = item.Product.Description,
                ImageUrl = item.ImageUrl,
                UnitPrice = item.Price
            };
    }
}