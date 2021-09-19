using System;

namespace Messages.Accounts.Commands
{
    public interface ChangeAccountPassword
    {
        Guid AccountId { get; }
        string NewPassword { get; }
        string NewPasswordConfirmation { get; }
    }
}