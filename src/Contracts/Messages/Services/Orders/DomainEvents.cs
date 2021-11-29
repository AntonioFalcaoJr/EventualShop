using System;
using System.Collections.Generic;
using Messages.Abstractions.Events;

namespace Messages.Services.Orders;

public static class DomainEvents
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total, IEnumerable<Models.Item> Items, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event;
}