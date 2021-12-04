using System;
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.ShoppingCart;

public static class Queries
{
    public record GetShoppingCart(Guid UserId) : Query;
}