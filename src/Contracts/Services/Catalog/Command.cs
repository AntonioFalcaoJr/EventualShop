using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Command
{
    public record AddCatalogItem(Guid CatalogId, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity) : Message(CorrelationId: CatalogId), ICommand;

    public record ActivateCatalog(Guid CatalogId) : Message(CorrelationId: CatalogId), ICommand;

    public record CreateCatalog(Guid CatalogId, string Title, string Description) : Message(CorrelationId: CatalogId), ICommand;

    public record ChangeCatalogTitle(Guid CatalogId, string Title) : Message(CorrelationId: CatalogId), ICommand;

    public record ChangeCatalogDescription(Guid CatalogId, string Description) : Message(CorrelationId: CatalogId), ICommand;

    public record DeactivateCatalog(Guid CatalogId) : Message(CorrelationId: CatalogId), ICommand;

    public record DeleteCatalog(Guid CatalogId) : Message(CorrelationId: CatalogId), ICommand;

    public record DeleteCatalogItem(Guid CatalogId, Guid CatalogItemId) : Message(CorrelationId: CatalogId), ICommand;
}