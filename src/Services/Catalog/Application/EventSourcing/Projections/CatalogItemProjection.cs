using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record CatalogItemProjection : IProjection
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string PictureUri { get; init; }
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}