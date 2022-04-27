using Contracts.Abstractions;
using Contracts.JsonConverters;
using MongoDB.Bson.Serialization.Attributes;

namespace Contracts.Services.Payments;

public static class Projection
{
    public record Payment : IProjection
    {
        public Guid OrderId { get; init; }
        public decimal Amount { get; init; }
        public AddressProjection BillingAddressProjection { get; init; }
        public CreditCardProjection CreditCardProjection { get; init; }
        public string Status { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record AddressProjection
    {
        public string City { get; init; }
        public string Country { get; init; }
        public int? Number { get; init; }
        public string State { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }

    public record CreditCardProjection
    {
        [BsonSerializer(typeof(ExpirationDateOnlyBsonSerializer))]
        public DateOnly Expiration { get; init; }
        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
    }
}