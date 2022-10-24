using Contracts.Abstractions.Messages;

namespace Contracts.Services.Identity;

public static class Query
{
    public record GetUserAuthentication(Guid UserId) : Message, IQuery;

    public record Login(string Email, string Password) : Message, IQuery;
}