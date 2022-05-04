using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive = default, bool IsDeleted = default) : IProjection;

    public record CatalogItem(Guid CatalogId, Guid Id, Dto.Product Product, bool IsDeleted) : IProjection;
}