using System;
using System.Collections.Generic;
using Messages.Abstractions.Events;

namespace Messages.Orders;

public static class Events
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, IEnumerable<Models.Item> Items, Models.Address BillingAddress, Models.CreditCard CreditCard, Models.Address ShippingAddress) : Event;
}