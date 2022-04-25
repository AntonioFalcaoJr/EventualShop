using Domain.Abstractions.Entities;
using Domain.Entities.Products;

namespace Domain.Entities.CatalogItems;

public class CatalogItem : Entity<Guid, CatalogItemValidator>
{
    public CatalogItem(Guid id, Product product, int quantity)
    {
        Id = id;
        Product = product;
        Quantity = quantity;
    }

    public Product Product { get; }
    public int Quantity { get; }
}