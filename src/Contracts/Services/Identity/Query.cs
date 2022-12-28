using Contracts.Abstractions.Messages;
using Contracts.Services.Identity.Protobuf;

namespace Contracts.Services.Identity;

public static class Query
{
    public record struct Login(string Email, string Password) : IQuery
    {
        public static implicit operator Login(LoginRequest request)
            => new(request.Email, request.Password);
    }
}