using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Warehouses;

public static class Projection
{
    public record Inventory : IProjection
    {
        public Models.Product Product { get; init; }
        public int Quantity { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}