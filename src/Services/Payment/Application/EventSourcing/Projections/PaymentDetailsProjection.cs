using System;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections;

public record PaymentDetailsProjection : IProjection
{
    public Guid Id { get; init; }
    public Guid OrderId { get; init; }
    public decimal Amount { get; init; }
    public AddressProjection BillingAddressProjection { get; init; }
    public CreditCardProjection CreditCardProjection { get; init; }
    public bool IsDeleted { get; init; }
    public string Status { get; init; }
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
    public DateOnly Expiration { get; init; }
    public string HolderName { get; init; }
    public string Number { get; init; }
    public string SecurityNumber { get; init; }
}