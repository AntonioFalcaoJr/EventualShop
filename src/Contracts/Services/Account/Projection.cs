using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Account.Grpc;

namespace Contracts.Services.Account;

public static class Projection
{
    public record AccountDetails(Guid Id, bool IsDeleted) : IProjection
    {
        public static implicit operator AccountResponse(AccountDetails accountDetails)
            => new()
            {
                Id = accountDetails.Id.ToString()
            };
    }

    public record BillingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : AddressListItem(Id, IsDeleted);

    public record ShippingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : AddressListItem(Id, IsDeleted);

    public abstract record AddressListItem(Guid Id, bool IsDeleted) : IProjection;
}