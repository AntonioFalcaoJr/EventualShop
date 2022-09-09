using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record AddEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record ConfirmEmail(Guid Id, string Email) : Message(CorrelationId: Id), ICommandWithId;

    public record RegisterUser(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), ICommandWithId;

    public record ChangePassword(Guid Id, string NewPassword, string NewPasswordConfirmation) : Message(CorrelationId: Id), ICommandWithId;

    public record DeleteUser(Guid Id) : Message(CorrelationId: Id), ICommandWithId;
}