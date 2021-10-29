using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Addresses;

public record Address : ValueObject
{
    public string City { get; init; }
    public string Country { get; init; }
    public int? Number { get; init; }
    public string State { get; init; }
    public string Street { get; init; }
    public string ZipCode { get; init; }

    protected override bool Validate()
        => OnValidate<AddressValidator, Address>();
}