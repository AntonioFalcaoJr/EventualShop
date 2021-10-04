using System;
using Domain.Abstractions.Entities;

namespace Domain.Entities.CartItems
{
    public class CartItem : Entity<Guid>
    {
        public CartItem(Guid productId, string productName, decimal unitPrice, int quantity)
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
        public int Quantity { get; private set; }
        public string PictureUrl { get; }

        public void IncreaseQuantity(int quantity) 
            => Quantity += quantity;

        protected override bool Validate()
            => OnValidate<CartItemValidator, CartItem>();
    }
}