using Contracts.Abstractions;

namespace Contracts.Services.Identity;

public static class Projection
{
    public record UserAuthentication(Guid Id, string Email, string Password, bool IsDeleted) : IProjection;
}