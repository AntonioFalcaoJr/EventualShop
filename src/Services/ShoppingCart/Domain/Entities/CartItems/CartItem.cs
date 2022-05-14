using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.CatalogItems;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<Guid, CartItemValidator>
{
    public CartItem(Guid id, Guid cartId, CatalogItem catalogItem, int quantity)
    {
        Id = id;
        CartId = cartId;
        CatalogItem = catalogItem;
        Quantity = quantity;
    }

    public Guid CartId { get; }
    public CatalogItem CatalogItem { get; }
    public int Quantity { get; private set; }

    public void Increase()
        => Quantity += 1;

    public void Decrease()
        => Quantity -= 1;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id, item.CartId, item.CatalogItem, item.Quantity);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.CartId, item.CatalogItem, item.Quantity);
}