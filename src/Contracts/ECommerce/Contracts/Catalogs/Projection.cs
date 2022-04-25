using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Catalogs;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection;

    public record CatalogItem(Guid CatalogId, Guid Id, Models.Product Product, bool IsDeleted) : IProjection;
}