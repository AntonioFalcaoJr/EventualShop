using ECommerce.Abstractions.Projections;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Accounts;

public static class Projection
{
    public record Account : IProjection
    {
        public Guid UserId { get; init; }
        public ProfileProjection Profile { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record ProfileProjection
    {
        public DateOnly? Birthdate { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public Models.Address ResidenceAddress { get; init; }
        public Models.Address ProfessionalAddress { get; init; }
    }
}