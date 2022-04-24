using ECommerce.Abstractions.Projections;

namespace ECommerce.Contracts.Orders;

public static class Projection
{
    public record Order : IProjection
    {
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}