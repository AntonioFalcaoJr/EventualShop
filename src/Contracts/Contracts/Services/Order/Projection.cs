using Contracts.Abstractions;

namespace Contracts.Services.Order;

public static class Projection
{
    public record Order : IProjection
    {
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}