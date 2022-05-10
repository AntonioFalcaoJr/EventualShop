using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.Addresses;

public record BillingAddress(string City, string Country, int? Number, string State, string Street, string ZipCode)
    : Address(City, Country, Number, State, Street, ZipCode)
{
    public static implicit operator BillingAddress(Dto.Address address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static implicit operator Dto.Address(BillingAddress address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);
}