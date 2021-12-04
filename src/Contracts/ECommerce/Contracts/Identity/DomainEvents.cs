using System;
using ECommerce.Abstractions.Events;

namespace ECommerce.Contracts.Identity;

public static class DomainEvents
{
    public record UserDeleted(Guid UserId) : Event;

    public record UserRegistered(Guid UserId, string Email, string FirstName, string Password, string PasswordConfirmation) : Event;

    public record UserPasswordChanged(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Event;
}