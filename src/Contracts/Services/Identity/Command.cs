using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record ChangeEmail(Guid Id, string Email) : Message, ICommand;

    public record ConfirmEmail(Guid Id, string Email) : Message, ICommand;

    public record ExpiryEmail(Guid Id, string Email) : Message, ICommand;

    public record RegisterUser(Guid Id, string FirstName, string LastName, string Email, string Password) : Message, ICommand;

    public record ChangePassword(Guid Id, string Password, string PasswordConfirmation) : Message, ICommand;

    public record DefinePrimaryEmail(Guid Id, string Email) : Message, ICommand;

    public record DeleteUser(Guid Id) : Message, ICommand;
}