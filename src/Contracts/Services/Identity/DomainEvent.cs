using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid UserId) : Message, IEvent;

    public record UserRegistered(Guid UserId, string FirstName, string LastName, string Email, string Password) : Message, IEvent;

    public record EmailChanged(Guid UserId, string Email) : Message, IEvent;

    public record UserPasswordChanged(Guid UserId, string Password) : Message, IEvent;

    public record EmailConfirmed(Guid UserId, string Email) : Message, IEvent;

    public record EmailExpired(Guid UserId, string Email) : Message, IEvent;

    public record PrimaryEmailDefined(Guid UserId, string Email) : Message, IEvent;
}