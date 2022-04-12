using ECommerce.Abstractions.Messages.Commands;

namespace ECommerce.Contracts.Identities;

public static class Commands
{
    public record RegisterUser(string Email, string FirstName, string Password, string PasswordConfirmation) : Command;

    public record ChangeUserPassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Command;

    public record DeleteUser(Guid UserId) : Command;
}