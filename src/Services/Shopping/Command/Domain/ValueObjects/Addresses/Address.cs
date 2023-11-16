using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.Addresses;

public record Address(City City, Complement Complement, Country Country, Number Number, State State, Street Street, ZipCode ZipCode)
{
    public static implicit operator Address(Dto.Address address)
        => new(address.Street, address.City, address.State, address.ZipCode, address.Country, address.Number, address.Complement);

    public static implicit operator Dto.Address(Address address)
        => new(address.Street, address.City, address.State, address.ZipCode, address.Country, address.Number, address.Complement);

    public static bool operator ==(Address address, Dto.Address dto)
        => dto == (Dto.Address)address;

    public static bool operator !=(Address address, Dto.Address dto)
        => dto != (Dto.Address)address;

    public static Address Undefined
        => new("Undefined", "Undefined", "Undefined", "Undefined", "Undefined", "Undefined", "Undefined");
}