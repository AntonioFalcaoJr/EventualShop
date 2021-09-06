using System;
using Application.Abstractions.EventSourcing.Projections;
using Messages.Customers.Queries.Responses;

namespace Application.EventSourcing.Customers.Projections
{
    public record CustomerDetailsProjection : CustomerDetails, IProjection
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
    }
}