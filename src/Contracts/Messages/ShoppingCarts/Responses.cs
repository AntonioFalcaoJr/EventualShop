using System;
using System.Collections.Generic;
using Messages.Abstractions.Queries.Responses;

namespace Messages.ShoppingCarts
{
    public static class Responses
    {
        public record CartDetails : Response
        {
            public IEnumerable<CartItem> CartItems { get; init; }
            public Guid UserId { get; init; }
            public decimal Total { get; init; }
            public Guid Id { get; init; }
            public bool IsDeleted { get; init; }
        }

        public record CartItem
        {
            public Guid CatalogItemId { get; init; }
            public string ProductName { get; init; }
            public decimal UnitPrice { get; init; }
            public int Quantity { get; init; }
            public string PictureUrl { get; init; }
        }
    }
}