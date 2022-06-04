using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Projection
{
    public record Catalog(Guid Id, string Title, string Description, bool IsActive, bool IsDeleted) : IProjection;

    public record CatalogItem(Guid CatalogId, Guid Id, Guid InventoryId, Dto.Product Product, bool IsDeleted) : IProjection;
}