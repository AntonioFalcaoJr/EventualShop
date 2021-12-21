using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record OrderDetailsProjection : IProjection
{
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}