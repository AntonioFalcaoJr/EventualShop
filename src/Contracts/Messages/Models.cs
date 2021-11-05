using System;
using Messages.JsonConverters;
using Newtonsoft.Json;

namespace Messages;

public static class Models
{
    public record Address
    {
        public string City { get; init; }
        public string Country { get; init; }
        public int? Number { get; init; }
        public string State { get; init; }
        public string Street { get; init; }
        public string ZipCode { get; init; }
    }

    public record CreditCard
    {
        [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))]
        public DateOnly Expiration { get; init; }
        public string HolderName { get; init; }
        public string Number { get; init; }
        public string SecurityNumber { get; init; }
    }

    public record Item
    {
        public Guid CatalogItemId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public string PictureUrl { get; init; }
    }

    public record Product
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public decimal UnitPrice { get; init; }
        public int QuantityOnHand { get; init; }
        public string PictureUrl { get; init; }
    }
}