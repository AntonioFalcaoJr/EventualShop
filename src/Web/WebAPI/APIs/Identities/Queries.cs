using Contracts.Services.Identity.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Queries
{
    public record SignIn(IdentityService.IdentityServiceClient Client, string Email, string Password, CancellationToken CancellationToken)
        : Validatable<SignInValidator>, IQuery<IdentityService.IdentityServiceClient>
    {
        public static implicit operator LoginRequest(SignIn request)
            => new() { Email = request.Email, Password = request.Password };
    }
}