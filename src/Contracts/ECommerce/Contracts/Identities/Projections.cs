using ECommerce.Abstractions.Projections;

namespace ECommerce.Contracts.Identities;

public static class Projections
{
    public record UserAuthentication : IProjection
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}