using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Accounts;

public static class Projection
{
    public record Account : IProjection
    {
        public Guid UserId { get; init; }
        public Profile Profile { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record Profile : IProjection
    {
        public DateOnly? Birthdate { get; init; }
        public string Email { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public Dto.Address ResidenceAddress { get; init; }
        public Dto.Address ProfessionalAddress { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}