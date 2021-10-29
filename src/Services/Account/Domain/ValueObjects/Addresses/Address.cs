using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Addresses;

public record Address(string City, string Country, int? Number, string State, string Street, string ZipCode) : ValueObject
{
    protected override bool Validate()
        => OnValidate<AddressValidator, Address>();
}