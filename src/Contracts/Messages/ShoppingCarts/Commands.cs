using System;
using Messages.Abstractions.Commands;

namespace Messages.ShoppingCarts
{
    public static class Commands
    {
        public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, Guid CartId, int Quantity) : Command;

        public record CreateCart(Guid CustomerId) : Command;
    }
}