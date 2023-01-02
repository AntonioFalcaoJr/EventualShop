using Contracts.DataTransferObjects;

namespace Domain.Entities.Addresses;

public class BillingAddress : Address
{
    public BillingAddress(Guid id, string street, string city, string state, string zipCode, string country, int? number, string? complement)
        : base(id, street, city, state, zipCode, country, number, complement) { }

    public static BillingAddress Create(Guid id, Dto.Address dto)
        => new(id, dto.Street, dto.City, dto.State, dto.ZipCode, dto.Country, dto.Number, dto.Complement);

    public static implicit operator Dto.Address(BillingAddress address)
        => new(address.Street, address.City, address.State, address.ZipCode, address.Country, address.Number, address.Complement);

    public static bool operator ==(BillingAddress address, Dto.Address dto)
        => dto == (Dto.Address)address;

    public static bool operator !=(BillingAddress address, Dto.Address dto)
        => dto != (Dto.Address)address;
}