using System;
using System.Collections.Generic;
using Application.Abstractions.EventSourcing.Projections;

namespace Application.EventSourcing.Projections
{
    public record CartDetailsProjection : IProjection
    {
        public IEnumerable<CartItemProjection> CartItems { get; init; }
        public Guid UserId { get; init; }
        public decimal Total { get; init; }
        public Guid Id { get; init; }
        public bool IsDeleted { get; init; }
    }

    public record CartItemProjection
    {
        public Guid CatalogItemId { get; init; }
        public string ProductName { get; init; }
        public decimal UnitPrice { get; init; }
        public int Quantity { get; init; }
        public string PictureUrl { get; init; }
    }
}