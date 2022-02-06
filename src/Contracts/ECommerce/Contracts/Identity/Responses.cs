using System;
using ECommerce.Abstractions.Messages.Queries.Responses;

namespace ECommerce.Contracts.Identity;

public static class Responses
{
    public record NotFound(string Message = "Not found.") : Response;

    public record UserAuthenticationDetails(Guid UserId, string Password, string UserName) : Response;
}