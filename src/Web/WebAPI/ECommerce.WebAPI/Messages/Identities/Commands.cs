using System;
using Messages.Identities.Commands;

namespace ECommerce.WebAPI.Messages.Identities
{
    public static class Commands
    {
        public record RegisterUserCommand(string Password, string PasswordConfirmation, string Login) : RegisterUser;

        public record ChangeUserPasswordCommand(Guid UserId, string NewPassword, string NewPasswordConfirmation) : ChangeUserPassword;

        public record DeleteUserCommand(Guid UserId) : DeleteUser;
    }
}