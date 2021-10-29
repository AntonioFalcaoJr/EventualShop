using System;
using System.Collections.Generic;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record CatalogProjection : IProjection
{
    public string Title { get; init; }
    public bool IsActive { get; init; }
    public IEnumerable<CatalogItemProjection> Items { get; init; }
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}