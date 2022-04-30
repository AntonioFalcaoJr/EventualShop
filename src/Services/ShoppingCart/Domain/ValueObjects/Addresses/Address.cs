using Contracts.DataTransferObjects;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Addresses;

public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode) : ValueObject<AddressValidator>
{
    public static implicit operator Address(Dto.Address address) 
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static implicit operator Dto.Address(Address address) 
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);
}