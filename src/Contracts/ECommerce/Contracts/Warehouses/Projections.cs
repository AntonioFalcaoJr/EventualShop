using ECommerce.Abstractions.Projections;

namespace ECommerce.Contracts.Warehouses;

public static class Projections
{
    public record Inventory : IProjection
    {
        public string Sku { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public int Quantity { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}