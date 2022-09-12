using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid Id) : Message(CorrelationId: Id), IEventWithId;

    public record UserRegistered(Guid Id, string FirstName, string LastName, string Email, string Password) : Message(CorrelationId: Id), IEventWithId;

    public record PasswordChanged(Guid Id, string NewPassword) : Message(CorrelationId: Id), IEventWithId;
    
    public record EmailConfirmed(Guid Id, string Email) : Message(CorrelationId: Id), IEventWithId;
}