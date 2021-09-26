using System;

namespace Messages.Identities.Queries
{
    public interface GetUserAuthenticationDetails
    {
        Guid UserId { get; }
    }
}