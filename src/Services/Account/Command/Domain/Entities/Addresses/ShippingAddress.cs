using Contracts.DataTransferObjects;

namespace Domain.Entities.Addresses;

public class ShippingAddress : Address
{
    public ShippingAddress(Guid id, string City, string Complement, string Country, string Number, string State, string Street, string ZipCode)
        : base(id, street, city, state, zipCode, country, number, complement) { }

    public static ShippingAddress Create(Guid id, Dto.Address dto)
        => new(id, dto.Street, dto.City, dto.State, dto.ZipCode, dto.Country, dto.Number, dto.Complement);

    public static implicit operator Dto.Address(ShippingAddress address)
        => new(address.Street, address.City, address.State, address.ZipCode, address.Country, address.Number, address.Complement);

    public static bool operator ==(ShippingAddress address, Dto.Address dto)
        => dto == (Dto.Address)address;

    public static bool operator !=(ShippingAddress address, Dto.Address dto)
        => dto != (Dto.Address)address;
}