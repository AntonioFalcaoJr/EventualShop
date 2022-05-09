using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Addresses;

// TODO - For the Account perspective, Address is an Entity. It can be edited by the User. Fix it!
public abstract record Address(string City, string Country, int? Number, string State, string Street, string ZipCode)
    : ValueObject<AddressValidator>;