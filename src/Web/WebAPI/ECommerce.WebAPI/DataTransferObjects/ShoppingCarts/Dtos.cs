using System;
using System.Collections.Generic;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public static class Dtos
{
    public record Cart(Guid Id, Guid CustomerId, IEnumerable<Item> Items, IEnumerable<string> PaymentMethods, decimal Total);

    public record Item(Guid Id, Guid ProductId, string ProductName, decimal UnitPrice, int Quantity, string PictureUrl);
}