using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record ChangeEmail(Guid UserId, string Email) : Message, ICommand;

    public record ConfirmEmail(Guid UserId, string Email) : Message, ICommand;

    public record ExpiryEmail(Guid UserId, string Email) : Message, ICommand;

    public record RegisterUser(Guid UserId, string FirstName, string LastName, string Email, string Password) : Message, ICommand;

    public record ChangePassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message, ICommand;

    public record DefinePrimaryEmail(Guid UserId, string Email) : Message, ICommand;

    public record DeleteUser(Guid UserId) : Message, ICommand;
}