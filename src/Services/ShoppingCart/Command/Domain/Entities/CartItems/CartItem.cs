using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<CartItemId, CartItemValidator>
{
    public CartItem(CartItemId id, Product product, int quantity, Money unitPrice)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Validate();
    }

    public Product Product { get; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; }

    public void SetQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), quantity, "Quantity must be greater than zero.");

        Quantity = quantity;
    }

    public void Delete()
        => IsDeleted = true;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id, item.Product, item.Quantity, item.UnitPrice);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.Product, item.Quantity, item.UnitPrice);
}