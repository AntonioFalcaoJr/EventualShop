using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<Guid, CartItemValidator>
{
    public CartItem(Guid id, Guid catalogId, Guid inventoryId, Product product, int quantity)
    {
        Id = id;
        CatalogId = catalogId;
        InventoryId = inventoryId;
        Product = product;
        Quantity = quantity;
    }

    public Guid CatalogId { get; }
    public Guid InventoryId { get; }
    public Product Product { get; }
    public int Quantity { get; private set; }

    public void Increase()
        => Quantity += 1;

    public void Decrease()
        => Quantity -= 1;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id, item.CatalogId, item.InventoryId, item.Product, item.Quantity);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.CatalogId, item.InventoryId, item.Product, item.Quantity);
}