namespace ECommerce.Contracts.Catalogs;

public static class Request
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);

    public record ChangeCatalogTitle(string Title);

    public record ChangeCatalogDescription(string Description);

    public record AddCatalogItem(string Name, string Description, decimal Price, string PictureUri);

    public record UpdateCatalogItem(string Name, string Description, decimal Price, string PictureUri);
}