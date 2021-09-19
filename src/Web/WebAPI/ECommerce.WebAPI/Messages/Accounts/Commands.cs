using System;
using Messages.Accounts.Commands;

namespace ECommerce.WebAPI.Messages.Accounts
{
    public static class Commands
    {
        public record RegisterAccountCommand(string Password, string PasswordConfirmation, string UserName) : RegisterAccount;

        public record ChangeAccountPasswordCommand(Guid AccountId, string NewPassword, string NewPasswordConfirmation) : ChangeAccountPassword;

        public record DeleteAccountCommand(Guid Id) : DeleteAccount;
    }
}