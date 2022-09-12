using Contracts.Abstractions.Messages;
using Contracts.Query;
using Contracts.Services.Identity;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Requests
{
    public record Login(IdentityService.IdentityServiceClient Client, string Email, string Password, CancellationToken CancellationToken)
        : Validatable<LoginValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    {
        public static implicit operator LoginRequest(Login request)
            => new() { Email = request.Email, Password = request.Password };
    }

    public record RegisterUser(IBus Bus, Payloads.RegisterUser Payload, CancellationToken CancellationToken)
        : Validatable<RegisterUserValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RegisterUser(Guid.NewGuid(), Payload.FirstName, Payload.LastName, Payload.Email, Payload.Password);
    }

    public record ChangeEmail(IBus Bus, Guid UserId, string Email, CancellationToken CancellationToken)
        : Validatable<ChangeEmailValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ChangeEmail(UserId, Email);
    }

    // public record RefreshToken(IdentityService.IdentityServiceClient Client, string Token, string RefreshToken, CancellationToken CancellationToken)
    //     : Validatable<RefreshTokenValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    // {
    //     public static implicit operator RefreshTokenRequest(RefreshToken request)
    //         => new() { Token = request.Token, RefreshToken = request.RefreshToken };
    // }
    //
    // public record RevokeToken(IdentityService.IdentityServiceClient Client, string Token, string RefreshToken, CancellationToken CancellationToken)
    //     : Validatable<RevokeTokenValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    // {
    //     public static implicit operator RevokeTokenRequest(RevokeToken request)
    //         => new() { Token = request.Token, RefreshToken = request.RefreshToken };
    // }
}