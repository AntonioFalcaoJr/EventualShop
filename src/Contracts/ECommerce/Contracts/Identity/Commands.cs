using ECommerce.Abstractions.Commands;

namespace ECommerce.Contracts.Identity;

public static class Commands
{
    public record RegisterUser(string Email, string FirstName, string Password, string PasswordConfirmation) : Command;

    public record ChangeUserPassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Command;

    public record DeleteUser(Guid UserId) : Command;
}