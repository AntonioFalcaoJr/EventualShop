using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.CartItems
{
    public class ShoppingCartItem : Entity<Guid>
    {
        public ShoppingCartItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public Guid ProductId { get; }
        public string ProductName { get; }
        public decimal UnitPrice { get; }
        public int Quantity { get; }
        public string PictureUrl { get; }

        protected override bool Validate()
            => throw new NotImplementedException();
    }
}