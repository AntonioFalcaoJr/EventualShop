using System;
using System.Collections.Generic;
using Messages.Abstractions.Events;

namespace Messages.Services.ShoppingCarts;

public static class IntegrationEvents
{
    public record CartSubmitted(Guid CartId, Guid CustomerId, decimal Total, IEnumerable<Models.Item> CartItems, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event;
}