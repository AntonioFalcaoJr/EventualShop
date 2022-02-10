using System;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public static class Requests
{
    public record CreateCart(Guid CustomerId);
}