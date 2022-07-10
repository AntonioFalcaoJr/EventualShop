using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Projection
{
    public record Account(Guid Id, Dto.Profile Profile, bool IsDeleted) : IProjection;

    public record BillingAddress(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : Address(Id, IsDeleted);

    public record ShippingAddress(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : Address(Id, IsDeleted);

    public abstract record Address(Guid Id, bool IsDeleted) : IProjection;
}