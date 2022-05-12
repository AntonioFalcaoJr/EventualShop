using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouse;

public static class Projection
{
    public record Inventory(Guid Id, Guid OwnerId, bool IsDeleted) : IProjection;

    public record InventoryItem(Guid Id, Guid InventoryId, Dto.Product Product, int Quantity, bool IsDeleted) : IProjection;
}