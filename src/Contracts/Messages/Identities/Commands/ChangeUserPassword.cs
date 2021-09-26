using System;

namespace Messages.Identities.Commands
{
    public interface ChangeUserPassword
    {
        Guid UserId { get; }
        string NewPassword { get; }
        string NewPasswordConfirmation { get; }
    }
}