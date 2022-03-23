using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record CatalogItemsProjection : IProjection
{
    public Guid Id { get; init; }
    public IEnumerable<CatalogItemProjection> Items { get; init; }
    public bool IsDeleted { get; init; }
}