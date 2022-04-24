﻿using ECommerce.Abstractions;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.ShoppingCarts;

public static class IntegrationEvent
{
    public record CartSubmitted(Guid ShoppingCartId, Models.Customer Customer, decimal Total, IEnumerable<Models.ShoppingCartItem> ShoppingCartItems, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Message(CorrelationId: ShoppingCartId), IEvent;
}