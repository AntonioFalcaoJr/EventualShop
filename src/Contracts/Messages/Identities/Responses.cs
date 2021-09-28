using System;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Identities
{
    public static class Responses
    {
        public record UserAuthenticationDetails(Guid Id, string Password, string UserName) : Response;
    }
}