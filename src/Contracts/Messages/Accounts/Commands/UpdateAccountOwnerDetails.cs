using System;

namespace Messages.Accounts.Commands
{
    public interface UpdateAccountOwnerDetails
    {
        Guid AccountId { get; }
        Guid OwnerId { get; }
        int Age { get; }
        string Email { get; }
        string LastName { get; }
        string Name { get; }
    }
}