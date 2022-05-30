using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.Enumerations;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<Guid, CartItemValidator>
{
    public CartItem(Guid id, Guid catalogId, Product product, int quantity, string sku, decimal unitPrice)
    {
        Id = id;
        CatalogId = catalogId;
        Product = product;
        Quantity = quantity;
        Sku = sku;
        UnitPrice = unitPrice;
        Status = ItemStatus.Unconfirmed;
    }

    public Guid CatalogId { get; }
    public Product Product { get; }
    public int Quantity { get; private set; }
    public string Sku { get; }
    public decimal UnitPrice { get; }
    public ItemStatus Status { get; private set; }

    public void Increase()
        => Quantity += 1;

    public void Decrease()
        => Quantity -= 1;

    public void Confirm()
        => Status = ItemStatus.Confirmed;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id, item.CatalogId, item.Product, item.Quantity, item.Sku, item.UnitPrice);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.CatalogId, item.Product, item.Quantity, item.Sku, item.UnitPrice);
}