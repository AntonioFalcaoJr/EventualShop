using System;
using System.Collections.Generic;
using Messages.Abstractions.Commands;

namespace Messages.Services.Orders;

public static class Commands
{
    public record PlaceOrder(Guid CustomerId, IEnumerable<Models.Item> Items, Models.Address BillingAddress, Models.CreditCard CreditCard, Models.Address ShippingAddress) : Command;
}