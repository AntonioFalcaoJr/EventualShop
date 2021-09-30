using System;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Identities
{
    public static class Responses
    {
        public record UserAuthenticationDetails(Guid UserId, string Password, string UserName) : Response;
    }
}