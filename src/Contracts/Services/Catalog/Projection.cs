using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Catalog.Protobuf;

namespace Contracts.Services.Catalog;

public static class Projection
{
    public record CatalogDetails(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.Catalog(CatalogDetails catalog)
            => new()
            {
                Id = catalog.Id.ToString(),
                Title = catalog.Title,
                Description = catalog.Description,
                IsActive = catalog.IsActive,
                IsDeleted = catalog.IsDeleted
            };
    }

    public record CatalogItemListItem(Guid CatalogId, Guid Id, Guid InventoryId, Dto.Product Product, bool IsDeleted) : IProjection
    {
        public static implicit operator CatalogItem(CatalogItemListItem item)
            => new()
            {
                Id = item.Id.ToString(),
                CatalogId = item.CatalogId.ToString(),
                InventoryId = item.InventoryId.ToString(),
                Product = (Product)item.Product,
                IsDeleted = item.IsDeleted
            };
    }
}