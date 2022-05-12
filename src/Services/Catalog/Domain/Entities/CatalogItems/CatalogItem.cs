using Domain.Abstractions.Entities;
using Domain.ValueObjects.Products;

namespace Domain.Entities.CatalogItems;

public class CatalogItem : Entity<Guid, CatalogItemValidator>
{
    public CatalogItem(Guid id, Guid inventoryId, Product product, int quantity)
    {
        Id = id;
        InventoryId = inventoryId;
        Product = product;
        Quantity = quantity;
    }

    public Guid InventoryId { get; }
    public Product Product { get; }
    public int Quantity { get; }
}