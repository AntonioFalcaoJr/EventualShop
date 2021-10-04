using System;
using Messages.Abstractions.Commands;

namespace Messages.ShoppingCarts
{
    public static class Commands
    {
        public record AddShoppingCartItem(Guid CatalogItemId, string CatalogItemName, decimal UnitPrice, Guid ShoppingCartId, int Quantity) : Command;

        public record CreateShoppingCart(Guid CustomerId) : Command;
    }
}