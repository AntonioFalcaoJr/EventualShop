using System;

namespace Messages.Identities
{
    public static class Responses
    {
        public record UserAuthenticationDetails(Guid Id, string Password, string UserName);
    }
}