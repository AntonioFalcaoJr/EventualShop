using Contracts.DataTransferObjects;

namespace Domain.ValueObjects.Products;

public record Product(string Description, string Name, string Brand, string Category, string Unit, string Sku)
{
    public static implicit operator Product(Dto.Product product)
        => new(product.Description, product.Name, product.Brand, product.Category, product.Unit, product.Sku);

    public static implicit operator Dto.Product(Product product)
        => new(product.Description, product.Name, product.Brand, product.Category, product.Unit, product.Sku);

    public static bool operator ==(Product product, Dto.Product dto)
        => dto == (Dto.Product)product;

    public static bool operator !=(Product product, Dto.Product dto)
        => dto != (Dto.Product)product;
}