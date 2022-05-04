using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Projection
{
    public record Account(Guid Id, Guid UserId, Dto.Profile Profile, bool IsDeleted) : IProjection;
}