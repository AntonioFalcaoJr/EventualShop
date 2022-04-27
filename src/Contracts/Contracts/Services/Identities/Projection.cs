using Contracts.Abstractions;

namespace Contracts.Services.Identities;

public static class Projection
{
    public record UserAuthentication : IProjection
    {
        public string Email { get; init; }
        public string Password { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }
}