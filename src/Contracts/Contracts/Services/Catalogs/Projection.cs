using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalogs;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection;

    public record CatalogItem(Guid CatalogId, Guid Id, Dto.Product Product, bool IsDeleted) : IProjection;
}