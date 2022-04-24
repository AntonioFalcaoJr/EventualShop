using ECommerce.Abstractions;

namespace ECommerce.Contracts.Catalogs;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection;

    public record CatalogItem(Guid CatalogId, Guid Id, string Name, string Description, decimal Price, string PictureUri, bool IsDeleted) : IProjection;
}