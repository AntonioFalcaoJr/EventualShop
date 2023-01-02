using Domain.Abstractions.Entities;

namespace Domain.Entities.Addresses;

public abstract class Address : Entity<AddressValidator>
{
    protected Address(Guid id, string street, string city, string state, string zipCode, string country, int? number, string? complement)
    {
        Id = id;
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
        Country = country;
        Number = number;
        Complement = complement;
    }

    public string Street { get; }
    public string City { get; }
    public string State { get; }
    public string ZipCode { get; }
    public string Country { get; }
    public int? Number { get; }
    public string? Complement { get; }

    public void Delete()
        => IsDeleted = true;

    public void Restore()
        => IsDeleted = false;
}