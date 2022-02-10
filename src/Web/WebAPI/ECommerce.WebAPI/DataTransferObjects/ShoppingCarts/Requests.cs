using System;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public abstract class Requests
{
    public record CreateCart(Guid CustomerId);
}