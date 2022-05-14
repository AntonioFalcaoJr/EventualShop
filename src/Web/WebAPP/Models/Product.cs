using Contracts.DataTransferObjects;

namespace WebAPP.Models;

public record Product
{
    public Product(string description, string name, decimal unitPrice, string pictureUrl, string sku)
    {
        Description = description;
        Name = name;
        UnitPrice = unitPrice;
        PictureUrl = pictureUrl;
        Sku = sku;
    }

    public Guid Id { get; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal UnitPrice { get; set; }
    public string PictureUrl { get; set; }
    public string Sku { get; set; }

    public static implicit operator Product(Dto.Product product)
        => new(product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);

    public static implicit operator Dto.Product(Product product)
        => new(product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);
}