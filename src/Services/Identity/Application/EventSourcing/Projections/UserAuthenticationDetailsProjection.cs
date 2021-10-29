using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record UserAuthenticationDetailsProjection : IProjection
{
    public string Email { get; init; }
    public string Password { get; init; }
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}