using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record ChangeEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record VerifyEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record RegisterUser(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), ICommandWithId;

    public record ChangePassword(Guid Id, string Password, string PasswordConfirmation) : Message(CorrelationId: Id), ICommandWithId;

    public record DefinePrimaryEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record DeleteUser(Guid Id) : Message(CorrelationId: Id), ICommandWithId;
}