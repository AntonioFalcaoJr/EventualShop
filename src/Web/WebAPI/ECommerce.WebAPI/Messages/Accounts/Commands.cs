using System;
using Messages.Accounts.Commands;

namespace ECommerce.WebAPI.Messages.Accounts
{
    public static class Commands
    {
        public record RegisterAccountCommand(string Name, int Age) : RegisterAccount;

        public record UpdateAccountCommand(Guid Id, string Name, int Age) : UpdateAccount;

        public record DeleteAccountCommand(Guid Id) : DeleteAccount;
    }
}