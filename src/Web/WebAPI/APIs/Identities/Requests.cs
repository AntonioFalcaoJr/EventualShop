using Contracts.Query;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Requests
{
    public record Login(IdentityService.IdentityServiceClient Client, string Email, string Password, CancellationToken CancellationToken)
        : Validatable<LoginValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    {
        public static implicit operator LoginRequest(Login request)
            => new() {Email = request.Email, Password = request.Password};
    }
}