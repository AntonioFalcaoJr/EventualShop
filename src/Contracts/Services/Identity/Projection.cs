using Contracts.Abstractions;
using Contracts.Query;

namespace Contracts.Services.Identity;

public static class Projection
{
    public record UserDetails(Guid Id, string FirstName, string LastName, string Email, string Password, string Token, bool IsDeleted) : IProjection
    {
        public static implicit operator LoginResponse(UserDetails userDetails) => new() {Token = userDetails.Token};
    }
}