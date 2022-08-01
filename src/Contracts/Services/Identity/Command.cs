using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Command
{
    public record RegisterUser(string FirstName, string LastName, string Email, string Password, string PasswordConfirmation) : Message, ICommand;

    public record ChangePassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message, ICommand;

    public record DeleteUser(Guid UserId) : Message, ICommand;
}