using System;
using System.Collections.Generic;
using Messages.Abstractions.Events;

namespace Messages.Services.Orders;

public static class DomainEvents
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, IEnumerable<Models.Item> Items, Models.Address BillingAddress, Models.CreditCard CreditCard, Models.Address ShippingAddress) : Event;
}