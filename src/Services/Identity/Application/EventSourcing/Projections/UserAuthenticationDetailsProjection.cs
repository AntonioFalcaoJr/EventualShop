using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections
{
    public record UserAuthenticationDetailsProjection : IProjection
    {
        public Guid Id { get; init; }
        public string Password { get; init; }
        public bool IsDeleted { get; init; }
        public string Login { get; init; }
    }
}