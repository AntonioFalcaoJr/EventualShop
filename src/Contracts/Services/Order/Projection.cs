using Contracts.Abstractions;

namespace Contracts.Services.Order;

public static class Projection
{
    public record Order(Guid Id, bool IsDeleted) : IProjection;
}