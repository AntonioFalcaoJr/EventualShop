using System;
using Messages.Abstractions.Events;
using Messages.JsonConverters;
using Newtonsoft.Json;

namespace Messages.Services.ShoppingCarts;

public static class DomainEvents
{
    public record CartCreated(Guid CartId, Guid UserId) : Event;

    public record CartItemAdded(Guid CartId, Models.Product Product, int Quantity) : Event;

    public record CartItemQuantityIncreased(Guid CartId, Guid ProductId, int Quantity) : Event;

    public record CartItemRemoved(Guid CartId, Guid CatalogItemId) : Event;

    public record CartCheckedOut(Guid CartId) : Event;

    public record ShippingAddressAdded(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

    public record BillingAddressChanged(Guid CartId, string City, string Country, int? Number, string State, string Street, string ZipCode) : Event;

    public record CreditCardAdded(Guid CartId, [property: JsonConverter(typeof(ExpirationDateOnlyJsonConverter))] DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : Event;
}