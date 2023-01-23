using Contracts.DataTransferObjects;

namespace WebAPI.APIs.Catalogs;

public static class Payloads
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record AddCatalogItem(Guid InventoryId, Dto.Product Product, string UnitPrice, string Sku, int Quantity);
}