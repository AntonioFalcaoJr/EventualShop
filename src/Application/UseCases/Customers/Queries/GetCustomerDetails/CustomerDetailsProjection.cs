using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.UseCases.Customers.Queries.GetCustomerDetails
{
    public record CustomerDetailsProjection : IProjection
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public int Age { get; init; }
    }
}