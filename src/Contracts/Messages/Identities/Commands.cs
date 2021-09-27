using System;
using Messages.Abstractions;

namespace Messages.Identities
{
    public static class Commands
    {
        public record RegisterUser(string Email, string FirstName, string Password, string PasswordConfirmation) : ICommand;

        public record ChangeUserPassword(Guid UserId, string NewPassword, string NewPasswordConfirmation) : ICommand;

        public record DeleteUser(Guid UserId) : ICommand;
    }
}