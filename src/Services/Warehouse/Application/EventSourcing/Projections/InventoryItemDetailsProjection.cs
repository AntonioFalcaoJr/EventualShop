using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record InventoryItemDetailsProjection : IProjection
{
    public string Sku { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public int Quantity { get; init; }
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}