using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;

namespace Contracts.Services.Catalog;

public static class Command
{
    public record AddCatalogItem(Guid Id, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity) : Message(CorrelationId: Id), ICommand;

    public record ActivateCatalog(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record CreateCatalog(Guid Id, string Title, string Description) : Message(CorrelationId: Id), ICommand;

    public record ChangeCatalogTitle(Guid Id, string Title) : Message(CorrelationId: Id), ICommand;

    public record ChangeCatalogDescription(Guid Id, string Description) : Message(CorrelationId: Id), ICommand;

    public record DeactivateCatalog(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record DeleteCatalog(Guid Id) : Message(CorrelationId: Id), ICommand;

    public record RemoveCatalogItem(Guid Id, Guid ItemId) : Message(CorrelationId: Id), ICommand;
}