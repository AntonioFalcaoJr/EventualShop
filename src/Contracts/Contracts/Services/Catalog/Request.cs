using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Request
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record AddCatalogItem(Dto.Product Product, int Quantity);
}