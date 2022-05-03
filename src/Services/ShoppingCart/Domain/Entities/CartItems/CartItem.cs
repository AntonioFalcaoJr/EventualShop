using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.Entities.Products;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<Guid, CartItemValidator>
{
    public CartItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; private set; }

    public void Increase()
        => Quantity += 1;

    public void Decrease()
        => Quantity -= 1;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id ?? default, item.Product, item.Quantity);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.Product, item.Quantity);
}