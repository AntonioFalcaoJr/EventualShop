using System;
using Application.Abstractions.EventSourcing.Projections;
using Messages.Accounts.Queries.Responses;

namespace Application.EventSourcing.Projections
{
    public record AccountAuthenticationDetailsProjection : AccountDetails, IProjection
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public string Password { get; init; }
        public bool IsDeleted { get; init; }
        public string UserName { get; init; }
    }
}