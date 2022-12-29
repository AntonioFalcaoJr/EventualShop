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
    public record CreateCatalog(IBus Bus, Payloads.CreateCatalog Payload, CancellationToken CancellationToken)
        : Validatable<CreateCatalogValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.CreateCatalog(Payload.CatalogId, Payload.Title, Payload.Description);
    }

    public record AddCatalogItem(IBus Bus, Guid CatalogId, Payloads.AddCatalogItem Payload, CancellationToken CancellationToken)
        : Validatable<AddCatalogItemValidator>, ICommandRequest
    {
        public ICommand Command
            => new Command.AddCatalogItem(CatalogId, Payload.InventoryId, Payload.Product, Payload.UnitPrice, Payload.Sku, Payload.Quantity);
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

    public record ListCatalogsGridItems(CatalogService.CatalogServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogsGridItemsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogsGridItemsRequest(ListCatalogsGridItems request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListCatalogItemsListItems(CatalogService.CatalogServiceClient Client, Guid CatalogId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogItemsListItemsRequestValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogItemsListItemsRequest(ListCatalogItemsListItems request)
            => new() { CatalogId = request.CatalogId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListCatalogItemsCards(CatalogService.CatalogServiceClient Client, Guid CatalogId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogItemsCardsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogItemsCardsRequest(ListCatalogItemsCards request)
            => new() { CatalogId = request.CatalogId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record GetCatalogItemDetails(CatalogService.CatalogServiceClient Client, Guid CatalogId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<GetCatalogItemDetailsValidator>, IQueryRequest<CatalogService.CatalogServiceClient>
    {
        public static implicit operator GetCatalogItemDetailsRequest(GetCatalogItemDetails request)
            => new() { CatalogId = request.CatalogId.ToString(), ItemId = request.ItemId.ToString() };
    }
}