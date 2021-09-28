using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections
{
    public record AccountDetailsProjection : IProjection
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public ProfileProjection Profile { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record ProfileProjection
    {
        public DateOnly? Birthdate { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public AddressProjection ResidenceAddress { get; init; }
        public AddressProjection ProfessionalAddress { get; init; }
    }

    public record AddressProjection
    {
        public string City { get; init; }
        public string Country { get; init; }
        public int? Number { get; init; }
        public string State { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }
}