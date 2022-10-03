using Contracts.DataTransferObjects;

namespace Domain.Entities.Addresses;

public class BillingAddress : Address
{
    public BillingAddress(Guid id, string city, string country, int? number, string state, string street, string zipCode)
        : base(id, city, country, number, state, street, zipCode) { }

    public static BillingAddress Create(Guid id, Dto.Address dto)
        => new(id, dto.City, dto.Country, dto.Number, dto.State, dto.Street, dto.ZipCode);

    public static implicit operator Dto.Address(BillingAddress address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static bool operator ==(BillingAddress address, Dto.Address dto)
        => dto == (Dto.Address)address;

    public static bool operator !=(BillingAddress address, Dto.Address dto)
        => dto != (Dto.Address)address;
}