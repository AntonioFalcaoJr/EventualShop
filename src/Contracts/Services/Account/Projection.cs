using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Projection
{
    public record AccountDetails(Guid Id, Dto.Profile Profile, bool IsDeleted) : IProjection;

    public record BillingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : AddressListItem(Id, IsDeleted);

    public record ShippingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : AddressListItem(Id, IsDeleted);

    public abstract record AddressListItem(Guid Id, bool IsDeleted) : IProjection;
}