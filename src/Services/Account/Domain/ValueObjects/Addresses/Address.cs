using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Addresses
{
    public record Address : ValueObject
    {
        public Address(string city, string country, int? number, string state, string street, string zipCode)
        {
            City = city;
            Country = country;
            Number = number;
            State = state;
            Street = street;
            ZipCode = zipCode;
        }

        public string City { get; }
        public string Country { get; }
        public int? Number { get; }
        public string State { get; }
        public string Street { get; }
        public string ZipCode { get; }

        protected override bool Validate()
            => OnValidate<AddressValidator, Address>();
    }
}