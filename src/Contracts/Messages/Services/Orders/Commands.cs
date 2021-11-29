using System;
using System.Collections.Generic;
using Messages.Abstractions.Commands;

namespace Messages.Services.Orders;

public static class Commands
{
    public record PlaceOrder(Guid CustomerId, IEnumerable<Models.Item> Items, decimal Total, Models.Address BillingAddress, Models.Address ShippingAddress, IEnumerable<Models.IPaymentMethod> PaymentMethods) : Command;
}