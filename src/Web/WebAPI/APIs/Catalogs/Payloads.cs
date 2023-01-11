using Contracts.DataTransferObjects;

namespace WebAPI.APIs.Catalogs;

public static class Payloads
{
    public record struct CreateCatalog(Guid CatalogId, string Title, string Description);

    public record struct AddCatalogItem(Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity);
}