using Domain.Abstractions.Entities;

namespace Domain.Entities.Products;

public class Product : Entity<Guid, ProductValidator>
{
    public Product(Guid id, string name, decimal unitPrice, string pictureUrl, string sku)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        PictureUrl = pictureUrl;
        Sku = sku;
    }

    public Guid Id { get; }
    public string Name { get; }
    public decimal UnitPrice { get; }
    public string PictureUrl { get; }
    public string Sku { get; }
}