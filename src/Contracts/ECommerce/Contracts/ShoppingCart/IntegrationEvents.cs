using System;
using System.Collections.Generic;
using ECommerce.Abstractions.Messages.Events;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCart;

public static class IntegrationEvents
{
    public record CartSubmitted(Guid CartId, Guid CustomerId, decimal Total, IEnumerable<Models.ShoppingCartItem> CartItems, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Event(CorrelationId: CartId);
}