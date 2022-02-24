using System;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public static class Requests
{
    public record CreateCart(Guid CustomerId);

    public record AddCartItem(Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl);
}