using Contracts.DataTransferObjects;

namespace WebAPP.ViewModels;

public record ProductViewModel
{
    public string? Description { get; set; }
    public string? Name { get; set; }
    public string? PictureUrl { get; set; }
    public string? Brand { get; set; }
    public string? Category { get; set; }
    public string? Unit { get; set; }
    public string? Sku { get; set; }

    public static implicit operator ProductViewModel(Dto.Product product)
        => new()
        {
            Brand = product.Brand,
            Category = product.Category,
            Description = product.Description,
            Name = product.Name,
            Unit = product.Unit,
            PictureUrl = product.PictureUrl,
            Sku = product.Sku
        };

    public static implicit operator Dto.Product(ProductViewModel productViewModel)
        => new(productViewModel.Description, productViewModel.Name, productViewModel.PictureUrl, productViewModel.Brand, productViewModel.Category, productViewModel.Unit, productViewModel.Sku);

    public static bool operator ==(ProductViewModel productViewModel, Dto.Product dto)
        => dto == (Dto.Product) productViewModel;

    public static bool operator !=(ProductViewModel productViewModel, Dto.Product dto)
        => dto != (Dto.Product) productViewModel;
}