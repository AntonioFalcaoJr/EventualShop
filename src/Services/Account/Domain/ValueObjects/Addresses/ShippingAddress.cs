using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.Addresses;

public record ShippingAddress(string City, string Country, int? Number, string State, string Street, string ZipCode)
    : Address(City, Country, Number, State, Street, ZipCode)
{
    public static implicit operator ShippingAddress(Dto.Address address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static implicit operator Dto.Address(ShippingAddress address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static bool operator ==(ShippingAddress address, Dto.Address dto)
        => dto == (Dto.Address) address;

    public static bool operator !=(ShippingAddress address, Dto.Address dto)
        => dto != (Dto.Address) address;
}