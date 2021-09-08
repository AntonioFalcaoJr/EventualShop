using System;

namespace Messages.Accounts.Commands
{
    public interface UpdateAccount
    {
        Guid Id { get; }
        string Name { get; }
        int Age { get; }
    }
}