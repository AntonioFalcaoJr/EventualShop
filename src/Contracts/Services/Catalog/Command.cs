using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Command
{
    public record AddCatalogItem(Guid CatalogId, Guid InventoryId, Dto.Product Product, Dto.Money UnitPrice, string Sku, int Quantity) : Message, ICommand;

    public record ActivateCatalog(Guid CatalogId) : Message, ICommand;

    public record CreateCatalog(Guid CatalogId, string Title, string Description) : Message, ICommand;

    public record ChangeCatalogTitle(Guid CatalogId, string Title) : Message, ICommand;

    public record ChangeCatalogDescription(Guid CatalogId, string Description) : Message, ICommand;

    public record DeactivateCatalog(Guid CatalogId) : Message, ICommand;

    public record DeleteCatalog(Guid CatalogId) : Message, ICommand;

    public record RemoveCatalogItem(Guid CatalogId, Guid ItemId) : Message, ICommand;
}