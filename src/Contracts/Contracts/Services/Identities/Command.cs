using Contracts.Abstractions;

namespace Contracts.Services.Identities;

public static class Command
{
    public record Register(string Email, string Password, string PasswordConfirmation) : Message, ICommand;

    public record ChangePassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : Message, ICommand;

    public record Delete(Guid UserId) : Message, ICommand;
}