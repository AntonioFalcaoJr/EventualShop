using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Warehouses;

public static class Projection
{
    public record Inventory : IProjection
    {
        public Dto.Product Product { get; init; }
        public int Quantity { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}