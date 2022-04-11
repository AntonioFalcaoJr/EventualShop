namespace ECommerce.Contracts.Catalogs;

public static class Requests
{
    public record CreateCatalog(Guid CatalogId, string Title, string Description);
}