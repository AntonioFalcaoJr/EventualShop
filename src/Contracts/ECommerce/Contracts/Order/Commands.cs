using System;
using System.Collections.Generic;
using ECommerce.Abstractions.Messages.Commands;
using ECommerce.Contracts.Common;

namespace ECommerce.Contracts.Order;

public static class Commands
{
    public record PlaceOrder(Guid CustomerId, IEnumerable<Models.ShoppingCartItem> Items, decimal Total, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Command(CorrelationId: CustomerId);

    public record ConfirmOrder(Guid OrderId) : Command(CorrelationId: OrderId);
}