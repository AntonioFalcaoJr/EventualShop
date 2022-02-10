using System;
using System.Collections.Generic;
using ECommerce.Contracts.Common;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public static class Outputs
{
    public record CartDetails(Guid Id, Guid CustomerId, IEnumerable<Models.Item> CartItems, IEnumerable<Models.IPaymentMethod> PaymentMethods, decimal Total);

    public record CartItemsDetailsPagedResult;
}