using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Request
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record AddCatalogItem(Guid InventoryId, Dto.Product Product, int Quantity);

    public record ChangeCatalogTitle(string Title);

    public record ChangeCatalogDescription(string Description);
}