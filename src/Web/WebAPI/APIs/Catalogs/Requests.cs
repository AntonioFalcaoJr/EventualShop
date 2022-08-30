using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Catalog;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Catalogs.Validators;

namespace WebAPI.APIs.Catalogs;

public static class Requests
{
    public record CreateCatalog(IBus Bus, Guid CatalogId, string Title, string Description, CancellationToken CancellationToken)
        : Validatable<CreateCatalogValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.CreateCatalog(CatalogId, Title, Description);
    }

    public record AddCatalogItem(IBus Bus, Guid CatalogId, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity, CancellationToken CancellationToken)
        : Validatable<AddCatalogItemValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.AddCatalogItem(CatalogId, InventoryId, Product, UnitPrice, Sku, Quantity);
    }

    public record DeleteCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeleteCatalogValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.DeleteCatalog(CatalogId);
    }

    public record ActivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<ActivateCatalogValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.ActivateCatalog(CatalogId);
    }

    public record DeactivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeactivateCatalogValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.DeactivateCatalog(CatalogId);
    }

    public record ChangeCatalogDescription(IBus Bus, Guid CatalogId, string Description, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogDescriptionValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.ChangeCatalogDescription(CatalogId, Description);
    }
    
    public record ChangeCatalogTitle(IBus Bus, Guid CatalogId, string Title, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogTitleValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.ChangeCatalogTitle(CatalogId, Title);
    }
    
    public record RemoveCatalogItem(IBus Bus, Guid CatalogId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCatalogItemValidator>, ICommandRequest
    {
        public ICommand AsCommand()
            => new Command.RemoveCatalogItem(CatalogId, ItemId);
    }
}