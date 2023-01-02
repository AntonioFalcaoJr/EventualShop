using Contracts.Abstractions;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Account;

public static class Projection
{
    public record AccountDetails(Guid Id, string FirstName, string LastName, string Email, string Status, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.AccountDetails(AccountDetails account)
            => new()
            {
                Id = account.Id.ToString(),
                FirstName = account.FirstName,
                LastName = account.LastName,
                Email = account.Email
            };
    }

    public record BillingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted)
        : AddressListItem(Id, AccountId, Address, IsDeleted);

    public record ShippingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted)
        : AddressListItem(Id, AccountId, Address, IsDeleted);

    public abstract record AddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : IProjection
    {
        public static implicit operator Protobuf.AddressListItem(AddressListItem item)
        {
            return new()
            {
                Id = item.Id.ToString(),
                AccountId = item.AccountId.ToString(),
                Address = item.Address
            };
        }
    }
}