using ECommerce.Abstractions;

namespace ECommerce.Contracts.Identities;

public static class DomainEvent
{
    public record UserDeleted(Guid UserId) : Message(CorrelationId: UserId), IEvent;

    public record UserRegistered(Guid UserId, string Email, string FirstName, string Password, string PasswordConfirmation) : Message(CorrelationId: UserId), IEvent;

    public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message(CorrelationId: UserId), IEvent;
}