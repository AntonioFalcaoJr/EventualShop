using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid UserId, long Version) : Message, IDomainEvent;

    public record UserRegistered(Guid UserId, string FirstName, string LastName, string Email, string Password, long Version) : Message, IDomainEvent;

    public record EmailChanged(Guid UserId, string Email, long Version) : Message, IDomainEvent;

    public record UserPasswordChanged(Guid UserId, string Password, long Version) : Message, IDomainEvent;

    public record EmailConfirmed(Guid UserId, string Email, long Version) : Message, IDomainEvent;

    public record EmailExpired(Guid UserId, string Email, long Version) : Message, IDomainEvent;

    public record PrimaryEmailDefined(Guid UserId, string Email, long Version) : Message, IDomainEvent;
}