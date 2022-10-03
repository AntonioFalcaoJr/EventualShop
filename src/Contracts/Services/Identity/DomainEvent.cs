using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid Id) : Message(CorrelationId: Id), IEvent;

    public record UserRegistered(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), IEvent;

    public record EmailChanged(Guid Id, string Email) : Message(CorrelationId: Id), IEvent;

    public record PasswordChanged(Guid Id, string Password) : Message(CorrelationId: Id), IEvent;

    public record EmailVerified(Guid Id, string Email) : Message(CorrelationId: Id), IEvent;

    public record EmailExpired(Guid Id, string Email) : Message(CorrelationId: Id), IEvent;

    public record PrimaryEmailDefined(Guid Id, string Email) : Message(CorrelationId: Id), IEvent;
}