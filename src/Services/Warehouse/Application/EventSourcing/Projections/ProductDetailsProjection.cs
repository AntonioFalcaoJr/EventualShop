using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record ProductDetailsProjection : IProjection
{
    public Guid Id { get; init; }
    public string Sku { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public bool IsDeleted { get; init; }
}