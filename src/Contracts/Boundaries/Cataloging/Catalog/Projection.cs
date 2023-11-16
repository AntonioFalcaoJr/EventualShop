using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Boundaries.Cataloging.Catalog;

public static class Projection
{
    public record CatalogGridItem(Guid Id, string Title, string Description, string ImageUrl, bool IsActive, bool IsDeleted, ulong Version) : IProjection
    {
        public static implicit operator Services.Cataloging.Query.Protobuf.CatalogGridItem(CatalogGridItem catalog)
            => new()
            {
                CatalogId = catalog.Id.ToString(),
                Title = catalog.Title,
                Description = catalog.Description,
                ImageUrl = catalog.ImageUrl,
                IsActive = catalog.IsActive
            };
    }

    public record CatalogItemListItem(Guid Id, Guid CatalogId, Guid ProductId, Dto.Product Product, bool IsDeleted, ulong Version) : IProjection
    {
        public static implicit operator Services.Cataloging.Query.Protobuf.CatalogItemListItem(CatalogItemListItem item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                ProductName = item.Product.Name
            };
    }

    public record CatalogItemCard(Guid Id, Guid CatalogId, Dto.Product Product, Dto.Money Price, string ImageUrl, bool IsDeleted, ulong Version) : IProjection
    {
        public static implicit operator Services.Cataloging.Query.Protobuf.CatalogItemCard(CatalogItemCard item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                Product = item.Product,
                Description = "item.Product.Description", // TODO
                ImageUrl = item.ImageUrl,
                UnitPrice = item.Price
            };
    }

    public record CatalogItemDetails(Guid Id, Guid CatalogId, Dto.Product Product, Dto.Money Price, string ImageUrl, bool IsDeleted, ulong Version) : IProjection
    {
        public static implicit operator Services.Cataloging.Query.Protobuf.CatalogItemDetails(CatalogItemDetails item)
            => new()
            {
                CatalogId = item.CatalogId.ToString(),
                ItemId = item.Id.ToString(),
                Product = item.Product,
                Description = "item.Product.Description", // TODO
                ImageUrl = item.ImageUrl,
                UnitPrice = item.Price
            };
    }
}