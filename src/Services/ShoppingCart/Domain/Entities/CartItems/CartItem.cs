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
}