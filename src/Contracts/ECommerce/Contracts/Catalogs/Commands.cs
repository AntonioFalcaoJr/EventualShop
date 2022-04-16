using ECommerce.Abstractions.Messages.Commands;

namespace ECommerce.Contracts.Catalogs;

public static class Commands
{
    public record CreateCatalog(string Title, string Description) : Command;

    public record DeleteCatalog(Guid CatalogId) : Command(CorrelationId: CatalogId);

    public record ChangeCatalogTitle(Guid CatalogId, string Title) : Command(CorrelationId: CatalogId);

    public record ChangeCatalogDescription(Guid CatalogId, string Description) : Command(CorrelationId: CatalogId);

    public record ActivateCatalog(Guid CatalogId) : Command(CorrelationId: CatalogId);

    public record DeactivateCatalog(Guid CatalogId) : Command(CorrelationId: CatalogId);

    public record DeleteCatalogItem(Guid CatalogId, Guid CatalogItemId) : Command(CorrelationId: CatalogId);

    public record AddCatalogItem(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri) : Command(CorrelationId: CatalogId);

    public record UpdateCatalogItem(Guid CatalogId, Guid CatalogItemId, string Name, string Description, decimal Price, string PictureUri) : Command(CorrelationId: CatalogId);
}