using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Command
{
    public record AddCatalogItem(Guid Id, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity) : Message, ICommand;

    public record ActivateCatalog(Guid Id) : Message, ICommand;

    public record CreateCatalog(Guid Id, string Title, string Description) : Message, ICommand;

    public record ChangeCatalogTitle(Guid Id, string Title) : Message, ICommand;

    public record ChangeCatalogDescription(Guid Id, string Description) : Message, ICommand;

    public record DeactivateCatalog(Guid Id) : Message, ICommand;

    public record DeleteCatalog(Guid Id) : Message, ICommand;

    public record RemoveCatalogItem(Guid Id, Guid ItemId) : Message, ICommand;
}