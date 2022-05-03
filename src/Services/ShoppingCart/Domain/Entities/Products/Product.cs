using Contracts.DataTransferObjects;
using Domain.Abstractions.Entities;

namespace Domain.Entities.Products;

public class Product : Entity<Guid, ProductValidator>
{
    public Product(Guid id, string description, string name, decimal unitPrice, string pictureUrl, string sku)
    {
        Id = id;
        Description = description;
        Name = name;
        UnitPrice = unitPrice;
        PictureUrl = pictureUrl;
        Sku = sku;
    }

    public string Name { get; }
    public string Description { get; }
    public decimal UnitPrice { get; }
    public string PictureUrl { get; }
    public string Sku { get; }

    public static implicit operator Product(Dto.Product product)
        => new(product.Id ?? default, product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);

    public static implicit operator Dto.Product(Product product)
        => new(product.Id, product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);
}