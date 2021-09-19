using System;

namespace Messages.Accounts.Queries.Responses
{
    public interface AccountDetails
    {
        Guid Id { get; }
        string Password { get; }
        string UserName { get; }
    }
}