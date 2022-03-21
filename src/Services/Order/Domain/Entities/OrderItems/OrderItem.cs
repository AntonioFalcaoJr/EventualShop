using Domain.Abstractions.Entities;

namespace Domain.Entities.OrderItems;

public class OrderItem : Entity<Guid>
{
    public OrderItem(Guid productId, string productName, string sku, string category, string brand, decimal price, int quantity, string pictureUrl)
    {
        Id = Guid.NewGuid();
        ProductId = productId;
        ProductName = productName;
        SKU = sku;
        Category = category;
        Brand = brand;
        Price = price;
        Quantity = quantity;
        PictureUrl = pictureUrl;
    }

    public Guid ProductId { get; }
    public string ProductName { get; }
    public string SKU { get; }
    public string Category { get; }
    public string Brand { get; }
    public decimal Price { get; }
    public int Quantity { get; }
    public string PictureUrl { get; }

    protected override bool Validate()
        => OnValidate<OrderItemValidator, OrderItem>();
}