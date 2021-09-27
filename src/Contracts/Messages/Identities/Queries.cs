using System;
using Messages.Abstractions;

namespace Messages.Identities
{
    public static class Queries
    {
        public record GetUserAuthenticationDetails(Guid UserId) : IQuery;
    }
}