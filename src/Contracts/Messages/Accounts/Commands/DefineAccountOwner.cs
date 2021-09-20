using System;

namespace Messages.Accounts.Commands
{
    public interface DefineAccountOwner
    {
        Guid AccountId { get; }
        int Age { get; }
        string Email { get; }
        string LastName { get; }
        string Name { get; }
    }
}