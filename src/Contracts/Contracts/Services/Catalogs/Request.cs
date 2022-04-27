using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalogs;

public static class Request
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record ChangeCatalogTitle(string Title);

    public record ChangeCatalogDescription(string Description);

    public record AddCatalogItem(Dto.Product Product, int Quantity);
}