using Contracts.Services.Catalog.Protobuf;
using WebAPI.Abstractions;
using WebAPI.APIs.Catalogs.Validators;

namespace WebAPI.APIs.Catalogs;

public static class Query
{
    public record ListCatalogsGridItems(CatalogService.CatalogServiceClient Client, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogsGridItemsValidator>, IQuery<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogsGridItemsRequest(ListCatalogsGridItems request)
            => new() { Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListCatalogItemsListItems(CatalogService.CatalogServiceClient Client, Guid CatalogId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogItemsListItemsRequestValidator>, IQuery<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogItemsListItemsRequest(ListCatalogItemsListItems request)
            => new() { CatalogId = request.CatalogId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record ListCatalogItemsCards(CatalogService.CatalogServiceClient Client, Guid CatalogId, int? Limit, int? Offset, CancellationToken CancellationToken)
        : Validatable<ListCatalogItemsCardsValidator>, IQuery<CatalogService.CatalogServiceClient>
    {
        public static implicit operator ListCatalogItemsCardsRequest(ListCatalogItemsCards request)
            => new() { CatalogId = request.CatalogId.ToString(), Paging = new() { Limit = request.Limit, Offset = request.Offset } };
    }

    public record GetCatalogItemDetails(CatalogService.CatalogServiceClient Client, Guid CatalogId, Guid ItemId, CancellationToken CancellationToken)
        : Validatable<GetCatalogItemDetailsValidator>, IQuery<CatalogService.CatalogServiceClient>
    {
        public static implicit operator GetCatalogItemDetailsRequest(GetCatalogItemDetails request)
            => new() { CatalogId = request.CatalogId.ToString(), ItemId = request.ItemId.ToString() };
    }
}