using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record ChangeEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommand;

    public record ConfirmEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommand;

    public record ExpiryEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommand;

    public record RegisterUser(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), ICommand;

    public record ChangePassword(Guid Id, string Password, string PasswordConfirmation) : Message(CorrelationId: Id), ICommand;

    public record DefinePrimaryEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommand;

    public record DeleteUser(Guid Id) : Message(CorrelationId: Id), ICommand;
}