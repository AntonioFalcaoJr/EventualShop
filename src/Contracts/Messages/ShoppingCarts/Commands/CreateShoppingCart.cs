using System;

namespace Messages.ShoppingCarts.Commands
{
    public interface CreateShoppingCart
    {
        Guid CustomerId { get; }
    }
}