using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<Guid, CartItemValidator>
{
    public CartItem(Guid id, Guid catalogId, Product product, ushort quantity, decimal unitPrice)
    {
        Id = id;
        CatalogId = catalogId;
        Product = product;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public Guid CatalogId { get; }
    public Product Product { get; }
    public ushort Quantity { get; private set; }
    public decimal UnitPrice { get; }

    public void SetQuantity(ushort quantity)
        => Quantity = quantity;

    public void Delete()
        => IsDeleted = true;

    public static implicit operator CartItem(Dto.CartItem item)
        => new(item.Id, item.CatalogId, item.Product, item.Quantity, item.UnitPrice);

    public static implicit operator Dto.CartItem(CartItem item)
        => new(item.Id, item.CatalogId, item.Product, item.Quantity, item.UnitPrice);
}