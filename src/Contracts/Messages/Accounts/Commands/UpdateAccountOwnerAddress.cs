using System;

namespace Messages.Accounts.Commands
{
    public interface UpdateAccountOwnerAddress
    {
        Guid AccountId { get; }
        Guid OwnerId { get; }
        string City { get; }
        string Country { get; }
        int? Number { get; }
        string State { get; }
        string Street { get; }
        string ZipCode { get; }
    }
}