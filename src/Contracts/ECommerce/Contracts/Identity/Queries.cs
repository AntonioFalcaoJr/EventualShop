using System;
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.Identity;

public static class Queries
{
    public record GetUserAuthenticationDetails(Guid UserId) : Query;
}