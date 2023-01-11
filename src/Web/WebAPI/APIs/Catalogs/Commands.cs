using Contracts.Services.Catalog;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Catalogs.Validators;

namespace WebAPI.APIs.Catalogs;

public static class Commands
{
    public record CreateCatalog(IBus Bus, Payloads.CreateCatalog Payload, CancellationToken CancellationToken)
        : Validatable<CreateCatalogValidator>, ICommand<Command.CreateCatalog>
    {
        public Command.CreateCatalog Command => new(Payload.CatalogId, Payload.Title, Payload.Description);
    }

    public record AddCatalogItem(IBus Bus, Guid CatalogId, Payloads.AddCatalogItem Payload, CancellationToken CancellationToken)
        : Validatable<AddCatalogItemValidator>, ICommand<Contracts.Services.Catalog.Command.AddCatalogItem>
    {
        public Command.AddCatalogItem Command
            => new(CatalogId, Payload.InventoryId, Payload.Product, Payload.UnitPrice, Payload.Sku, Payload.Quantity);
    }

    public record DeleteCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeleteCatalogValidator>, ICommand<Contracts.Services.Catalog.Command.DeleteCatalog>
    {
        public Command.DeleteCatalog Command => new(CatalogId);
    }

    public record ActivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<ActivateCatalogValidator>, ICommand<Contracts.Services.Catalog.Command.ActivateCatalog>
    {
        public Command.ActivateCatalog Command => new(CatalogId);
    }

    public record DeactivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeactivateCatalogValidator>, ICommand<Contracts.Services.Catalog.Command.DeactivateCatalog>
    {
        public Command.DeactivateCatalog Command => new(CatalogId);
    }

    public record ChangeCatalogDescription(IBus Bus, Guid CatalogId, string Description, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogDescriptionValidator>, ICommand<Command.ChangeCatalogDescription>
    {
        public Command.ChangeCatalogDescription Command => new(CatalogId, Description);
    }

    public record ChangeCatalogTitle(IBus Bus, Guid CatalogId, string Title, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogTitleValidator>, ICommand<Command.ChangeCatalogTitle>
    {
        public Command.ChangeCatalogTitle Command => new(CatalogId, Title);
    }

    public record RemoveCatalogItem(IBus Bus, Guid CatalogId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCatalogItemValidator>, ICommand<Command.RemoveCatalogItem>
    {
        public Command.RemoveCatalogItem Command => new(CatalogId, ItemId);
    }
}