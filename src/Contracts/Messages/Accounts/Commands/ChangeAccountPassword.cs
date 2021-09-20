using System;

namespace Messages.Accounts.Commands
{
    public interface ChangeAccountPassword
    {
        Guid AccountId { get; }
        Guid UserId { get; }
        string NewPassword { get; }
        string NewPasswordConfirmation { get; }
    }
}