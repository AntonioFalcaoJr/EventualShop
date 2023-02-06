using Contracts.Abstractions;
using MongoDB.Bson.Serialization.Attributes;

namespace Contracts.Services.Identity;

public static class Projection
{
    public record UserDetails(Guid Id, string FirstName, string LastName, string Email, string Password, bool IsDeleted, long Version, [property: BsonIgnore] string? Token = default) : IProjection
    {
        public static implicit operator Protobuf.UserDetails(UserDetails userDetails)
            => new()
            {
                UserId = userDetails.Id.ToString(),
                Email = userDetails.Email,
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Token = userDetails.Token
            };
    }
}