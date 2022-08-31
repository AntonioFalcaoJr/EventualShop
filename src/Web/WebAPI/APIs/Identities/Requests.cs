using Contracts.Abstractions.Messages;
using Contracts.Query;
using Contracts.Services.Identity;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Requests
{
    public record RegisterUser(IBus Bus, Payloads.RegisterUser Payload, CancellationToken CancellationToken)
        : Validatable<RegisterUserValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RegisterUser(Guid.NewGuid(), Payload.FirstName, Payload.LastName, Payload.Email, Payload.Password);
    }

    public record Login(IdentityService.IdentityServiceClient Client, string Email, string Password, CancellationToken CancellationToken)
        : Validatable<LoginValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    {
        public static implicit operator LoginRequest(Login request)
            => new() { Email = request.Email, Password = request.Password };
    }
}