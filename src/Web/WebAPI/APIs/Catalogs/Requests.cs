using Contracts.Abstractions.Messages;
using Contracts.DataTransferObjects;
using Contracts.Services.Catalog;
using Contracts.Services.Catalog.Protobuf;
using MassTransit;
using WebAPI.Abstractions;
using WebAPI.APIs.Catalogs.Validators;

namespace WebAPI.APIs.Catalogs;

public static class Requests
{
    public record CreateCatalog(IBus Bus, Guid CatalogId, string Title, string Description, CancellationToken CancellationToken)
        : Validatable<CreateCatalogValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CreateCatalog(CatalogId, Title, Description);
    }

    public record AddCatalogItem(IBus Bus, Guid CatalogId, Guid InventoryId, Dto.Product Product, decimal UnitPrice, string Sku, int Quantity, CancellationToken CancellationToken)
        : Validatable<AddCatalogItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddCatalogItem(CatalogId, InventoryId, Product, UnitPrice, Sku, Quantity);
    }

    public record DeleteCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeleteCatalogValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DeleteCatalog(CatalogId);
    }

    public record ActivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<ActivateCatalogValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ActivateCatalog(CatalogId);
    }

    public record DeactivateCatalog(IBus Bus, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<DeactivateCatalogValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.DeactivateCatalog(CatalogId);
    }

    public record ChangeCatalogDescription(IBus Bus, Guid CatalogId, string Description, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogDescriptionValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ChangeCatalogDescription(CatalogId, Description);
    }

    public record ChangeCatalogTitle(IBus Bus, Guid CatalogId, string Title, CancellationToken CancellationToken)
        : Validatable<ChangeCatalogTitleValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.ChangeCatalogTitle(CatalogId, Title);
    }

    public record RemoveCatalogItem(IBus Bus, Guid CatalogId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<RemoveCatalogItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.RemoveCatalogItem(CatalogId, ItemId);
    }
    
    public record GetCatalog(CatalogService.CatalogServiceClient Client, Guid CatalogId, CancellationToken CancellationToken)
        : Validatable<GetCatalogValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator GetCatalogRequest(GetCatalog request)
            => new() { Id = request.CatalogId.ToString() };
    }
    
    public record ListCatalogs(CatalogService.CatalogServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogsRequest(ListCatalogs request)
            => new() { Limit = request.Limit ?? default, Offset = request.Offset ?? default };
    }
    
    public record GetCatalogItems(CatalogService.CatalogServiceClient Client, Guid Id, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<GetCatalogItemsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator GetCatalogItemsRequest(GetCatalogItems request)
            => new() { Id  = request.Id.ToString(), Limit = request.Limit ?? default, Offset = request.Offset ?? default};
    }
    
    
    public record ListCatalogItems(CatalogService.CatalogServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogItemsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogItemsRequest(ListCatalogItems request)
            => new() { Limit = request.Limit ?? default, Offset = request.Offset ?? default};
    }
}