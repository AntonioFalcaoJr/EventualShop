using Domain.Abstractions.Entities;

namespace Domain.Entities.ShoppingCartItems;

public class ShoppingCartItem : Entity<Guid, ShoppingCartItemValidator>
{
    public ShoppingCartItem(Guid id, Guid productId, string productName, decimal unitPrice, int quantity, string pictureUrl, string sku)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        UnitPrice = unitPrice;
        Quantity = quantity;
        PictureUrl = pictureUrl;
        Sku = sku;
    }

    public Guid ProductId { get; }
    public string ProductName { get; }
    public decimal UnitPrice { get; }
    public int Quantity { get; private set; }
    public string PictureUrl { get; }
    public string Sku { get; }

    public void Increase()
        => Quantity += 1;

    public void Decrease()
        => Quantity -= 1;
}