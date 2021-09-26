using System;

namespace Messages.Identities.Queries.Responses
{
    public interface UserAuthenticationDetails
    {
        Guid Id { get; }
        string Password { get; }
        bool IsDeleted { get; }
        string Login { get; }
    }
}