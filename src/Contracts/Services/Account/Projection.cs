using Contracts.Abstractions;
using Contracts.DataTransferObjects;
using Contracts.Services.Account.Grpc;

namespace Contracts.Services.Account;

public static class Projection
{
    public record AccountDetails(Guid Id, string FirstName, string LastName, string Email, bool IsDeleted) : IProjection
    {
        public static implicit operator Grpc.Account(AccountDetails accountDetails)
            => new()
            {
                Id = accountDetails.Id.ToString(),
                FirstName = accountDetails.FirstName,
                LastName = accountDetails.LastName,
                Email = accountDetails.Email
            };
    }

    public record BillingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted)
        : AddressListItem(Id, AccountId, Address, IsDeleted);

    public record ShippingAddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted)
        : AddressListItem(Id, AccountId, Address, IsDeleted);

    public abstract record AddressListItem(Guid Id, Guid AccountId, Dto.Address Address, bool IsDeleted) : IProjection
    {
        public static implicit operator Address(AddressListItem listItem)
        {
            return new()
            {
                Id = listItem.Id.ToString(),
                AccountId = listItem.AccountId.ToString(),
                Street = listItem.Address.Street,
                City = listItem.Address.City,
                State = listItem.Address.State,
                ZipCode = listItem.Address.ZipCode
            };
        }
    }
}