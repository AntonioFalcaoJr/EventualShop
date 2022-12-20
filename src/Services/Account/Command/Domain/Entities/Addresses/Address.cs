using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;

namespace Domain.Entities.Addresses;

public abstract class Address : Entity<AddressValidator>
{
    protected Address(Guid id, string city, string country, int? number, string state, string street, string zipCode)
    {
        Id = id;
        City = city;
        Country = country;
        Number = number;
        State = state;
        Street = street;
        ZipCode = zipCode;
    }

    public string City { get; }
    public string Country { get; }
    public bool IsPreferred { get; private set; }
    public int? Number { get; }
    public string State { get; }
    public string Street { get; }
    public string ZipCode { get; }

    public void Prefer()
        => IsPreferred = true;

    public void Unprefer()
        => IsPreferred = false;

    public void Delete()
        => IsDeleted = true;

    public void Restore()
        => IsDeleted = false;

    public static implicit operator Dto.Address(Address address)
        => new(address.City, address.Country, address.Number, address.State, address.Street, address.ZipCode);

    public static implicit operator Dto.AddressItem(Address address)
        => new(address.Id, address);
}