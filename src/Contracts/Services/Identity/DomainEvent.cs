using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid Id) : Message(CorrelationId: Id), IEventWithId;

    public record UserRegistered(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), IEventWithId;

    public record EmailChanged(Guid Id, string Email) : Message(CorrelationId: Id), IEventWithId;

    public record PasswordChanged(Guid Id, string Password) : Message(CorrelationId: Id), IEventWithId;

    public record EmailVerified(Guid Id, string Email) : Message(CorrelationId: Id), IEventWithId;

    public record PrimaryEmailDefined(Guid Id, string Email) : Message(CorrelationId: Id), IEventWithId;
}