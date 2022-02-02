using System;
using System.Collections.Generic;
using ECommerce.Abstractions.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Order;

public static class DomainEvents
{
    public record OrderPlaced(Guid OrderId, Guid CustomerId, decimal Total, IEnumerable<Models.Item> Items, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event(CorrelationId: OrderId);

    public record OrderConfirmed(Guid OrderId) : Event(CorrelationId: OrderId);
}