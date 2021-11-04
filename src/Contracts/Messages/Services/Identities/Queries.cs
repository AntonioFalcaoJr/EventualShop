using System;
using Messages.Abstractions.Queries;

namespace Messages.Services.Identities;

public static class Queries
{
    public record GetUserAuthenticationDetails(Guid UserId) : Query;
}