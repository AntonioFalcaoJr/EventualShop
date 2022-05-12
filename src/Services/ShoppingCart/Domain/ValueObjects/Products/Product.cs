using Contracts.DataTransferObjects;
using Domain.Abstractions.ValueObjects;

namespace Domain.ValueObjects.Products;

public record Product(string Description, string Name, decimal UnitPrice, string PictureUrl, string Sku)
    : ValueObject<ProductValidator>
{
    public static implicit operator Product(Dto.Product product)
        => new(product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);

    public static implicit operator Dto.Product(Product product)
        => new(product.Description, product.Name, product.UnitPrice, product.PictureUrl, product.Sku);
}