using Contracts.DataTransferObjects;

namespace WebAPP.ViewModels;

public class CatalogItemViewModel
{
    public Guid Id { get; set; }
    public Guid InventoryId { get; set; }
    public ProductViewModel? ProductViewModel { get; set; }
    public Dto.Money UnitPrice { get; set; } = new("0.000", "Unknown");
    public string? Sku { get; set; }
    public int Quantity { get; private set; }

    public void Increase(int quantity)
        => Quantity += quantity;

    public void Decrease(int quantity)
        => Quantity += quantity;
}