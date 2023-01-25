using Domain.Abstractions.Entities;
using Domain.ValueObjects;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CatalogItems;

public class CatalogItem : Entity<CatalogItemValidator>
{
    public CatalogItem(Guid id, Guid inventoryId, Product product, Money unitPrice, string sku, int quantity)
    {
        Id = id;
        InventoryId = inventoryId;
        Product = product;
        UnitPrice = unitPrice;
        Sku = sku;
        Quantity = quantity;
    }

    public Guid InventoryId { get; }
    public Product Product { get; }
    public Money UnitPrice { get; }
    public string Sku { get; }
    public int Quantity { get; private set; }
    
    public Uri ImageUri => new($"https://localhost:5001/images/{Sku}.jpg"); // TODO: Move to config

    public void Increase(int quantity)
        => Quantity += quantity;

    public void Decrease(int quantity)
        => Quantity += quantity;
}