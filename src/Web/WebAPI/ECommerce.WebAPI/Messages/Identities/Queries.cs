using System;
using Messages.Identities.Queries;

namespace ECommerce.WebAPI.Messages.Identities
{
    public static class Queries
    {
        public record GetUserAuthenticationDetailsQuery(Guid UserId) : GetUserAuthenticationDetails;
    }
}