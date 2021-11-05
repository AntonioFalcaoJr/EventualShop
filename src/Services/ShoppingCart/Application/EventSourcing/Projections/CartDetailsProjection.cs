using System;
using System.Collections.Generic;
using Application.Abstractions.EventSourcing.Projections;
using MongoDB.Bson.Serialization.Attributes;

namespace Application.EventSourcing.Projections;

public record CartDetailsProjection : IProjection
{
    public IEnumerable<CartItemProjection> CartItems { get; init; }
    public Guid UserId { get; init; }
    public decimal Total { get; init; }
    public bool IsCheckedOut { get; init; }
    public AddressProjection ShippingAddressProjection { get; init; }
    public AddressProjection BillingAddressProjection { get; init; }
    public CreditCardProjection CreditCardProjection { get; init; }
    public Guid Id { get; init; }
    public bool IsDeleted { get; init; }
}

public record CartItemProjection
{
    public Guid CatalogItemId { get; init; }
    public string ProductName { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; init; }
    public string PictureUrl { get; init; }
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