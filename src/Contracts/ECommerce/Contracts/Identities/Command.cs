using ECommerce.Abstractions.Messages;
using ECommerce.Abstractions.Messages.Commands;

namespace ECommerce.Contracts.Identities;

public static class Command
{
    public record RegisterUser(string Email, string FirstName, string Password, string PasswordConfirmation) : Message, ICommand;

    public record ChangeUserPassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message, ICommand;

    public record DeleteUser(Guid UserId) : Message, ICommand;
}