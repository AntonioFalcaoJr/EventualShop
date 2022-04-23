using ECommerce.Abstractions.Projections;

namespace ECommerce.Contracts.Orders;

public static class Projections
{
    public record Order : IProjection
    {
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}