using System;

namespace Messages.Accounts.Commands
{
    public interface DeleteAccount
    {
        Guid AccountId { get; }
    }
}