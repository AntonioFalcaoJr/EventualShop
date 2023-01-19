using Contracts.DataTransferObjects;

namespace WebAPP.HttpClients;

public static class Requests
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record AddCatalogItem(Guid InventoryId, Dto.Product Product, string Sku, string UnitPrice, int Quantity);
}