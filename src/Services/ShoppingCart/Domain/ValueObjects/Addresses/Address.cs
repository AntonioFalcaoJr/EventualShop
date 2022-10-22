using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.Addresses;

public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode)
{
    public static implicit operator Address(Dto.Address address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static implicit operator Dto.Address(Address address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static bool operator ==(Address? address, Dto.Address dto)
        => address is not null && dto == (Dto.Address)address;

    public static bool operator !=(Address? address, Dto.Address dto)
        => address is null || dto != (Dto.Address)address;
}