using Contracts.DataTransferObjects;

namespace WebAPI.APIs.Catalogs;

public static class Payloads
{
    public record CreateCatalog(string Title, string Description);

    public record AddCatalogItem(Guid InventoryId, Dto.Product Product, Dto.Money UnitPrice, string Sku, int Quantity);
}