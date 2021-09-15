using System;
using Application.Abstractions.EventSourcing.Projections;
using Messages.Accounts.Queries.Responses;

namespace Application.EventSourcing.Projections
{
    public record AccountDetailsProjection : AccountDetails, IProjection
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
    }
}