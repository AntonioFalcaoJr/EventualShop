using Contracts.Abstractions.Messages;
using Contracts.Services.Identity;
using Contracts.Services.Identity.Protobuf;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Requests
{
    public record SignIn(IdentityService.IdentityServiceClient Client, string Email, string Password, CancellationToken CancellationToken)
        : Validatable<SignInValidator>, IQueryRequest<IdentityService.IdentityServiceClient>
    {
        public static implicit operator LoginRequest(SignIn request)
            => new() { Email = request.Email, Password = request.Password };
    }

    public record SignUp(IBus Bus, Payloads.SignUp Payload, CancellationToken CancellationToken)
        : Validatable<SignUpValidator>, ICommandRequest
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

    public record ChangePassword(IBus Bus, Guid UserId, string Password, string PasswordConfirmation, CancellationToken CancellationToken)
        : Validatable<ChangePasswordValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ChangePassword(UserId, Password, PasswordConfirmation);
    }

    public record ConfirmEmail(IBus Bus, Guid UserId, string Email, CancellationToken CancellationToken)
        : Validatable<ConfirmEmailValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ConfirmEmail(UserId, Email);
    }
}