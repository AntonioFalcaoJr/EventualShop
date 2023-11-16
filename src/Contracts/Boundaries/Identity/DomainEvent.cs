using Contracts.Abstractions.Messages;

namespace Contracts.Boundaries.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid UserId, string Version) : Message, IDomainEvent;

    public record UserRegistered(Guid UserId, string FirstName, string LastName, string Email, string Password, string Version) : Message, IDomainEvent;

    public record EmailChanged(Guid UserId, string Email, string Version) : Message, IDomainEvent;

    public record UserPasswordChanged(Guid UserId, string Password, string Version) : Message, IDomainEvent;

    public record EmailConfirmed(Guid UserId, string Email, string Version) : Message, IDomainEvent;

    public record EmailExpired(Guid UserId, string Email, string Version) : Message, IDomainEvent;

    public record PrimaryEmailDefined(Guid UserId, string Email, string Version) : Message, IDomainEvent;
}