using System;

namespace Messages.Accounts.Queries.Responses
{
    public interface AccountDetails
    {
        Guid Id { get; }
        string Name { get; }
        int Age { get; }
    }
}