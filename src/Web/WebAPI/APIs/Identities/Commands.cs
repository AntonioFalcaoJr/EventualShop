using Contracts.Services.Identity;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Identities.Validators;

namespace WebAPI.APIs.Identities;

public static class Commands
{
    public record RegisterUser(IBus Bus, Payloads.SignUp Payload, CancellationToken CancellationToken)
        : Validatable<SignUpValidator>, ICommand<Command.RegisterUser>
    {
        public Command.RegisterUser Command
            => new(Guid.NewGuid(), Payload.FirstName, Payload.LastName, Payload.Email, Payload.Password);
    }

    public record ChangeEmail(IBus Bus, Guid UserId, string Email, CancellationToken CancellationToken)
        : Validatable<ChangeEmailValidator>, ICommand<Command.ChangeEmail>
    {
        public Command.ChangeEmail Command => new(UserId, Email);
    }

    public record ChangePassword(IBus Bus, Guid UserId, string Password, string PasswordConfirmation, CancellationToken CancellationToken)
        : Validatable<ChangePasswordValidator>, ICommand<Command.ChangePassword>
    {
        public Command.ChangePassword Command => new(UserId, Password, PasswordConfirmation);
    }

    public record ConfirmEmail(IBus Bus, Guid UserId, string Email, CancellationToken CancellationToken)
        : Validatable<ConfirmEmailValidator>, ICommand<Command.ConfirmEmail>
    {
        public Command.ConfirmEmail Command => new(UserId, Email);
    }
}