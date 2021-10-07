using System;
using Messages.Abstractions.Queries;

namespace Messages.ShoppingCarts
{
    public static class Queries
    {
        public record GetShoppingCart(Guid UserId) : Query;
    }
}