using System;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.CartItems;

public record CartItem : ValueObject
{
    public Guid ProductId { get; init; }
    public string ProductName { get; init; }
    public decimal UnitPrice { get; init; }
    public int Quantity { get; set; }
    public string PictureUrl { get; init; }

    public void IncreaseQuantity(int quantity)
        => Quantity += quantity;

    public void DecreaseQuantity(int quantity)
        => Quantity -= quantity;

    protected override bool Validate()
        => OnValidate<CartItemValidator, CartItem>();
}