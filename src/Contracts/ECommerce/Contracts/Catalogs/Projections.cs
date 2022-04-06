using ECommerce.Abstractions.Projections;

namespace ECommerce.Contracts.Catalogs;

public static class Projections
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection;

    public record CatalogItem(Guid Id, Guid CatalogId, string Name, string Description, decimal Price, string PictureUri, bool IsDeleted) : IProjection;
}