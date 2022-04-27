using Contracts.Abstractions;

namespace Contracts.Services.Identity;

public static class DomainEvent
{
    public record UserDeleted(Guid UserId) : Message(CorrelationId: UserId), IEvent;

    public record UserRegistered(Guid UserId, string Email, string Password, string PasswordConfirmation) : Message(CorrelationId: UserId), IEvent;

    public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message(CorrelationId: UserId), IEvent;
}