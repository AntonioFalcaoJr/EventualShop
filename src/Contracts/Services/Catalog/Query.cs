using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog.Protobuf;
using Paging = Contracts.Abstractions.Paging.Paging;

namespace Contracts.Services.Catalog;

public static class Query
{
    public record ListCatalogsGridItems(Paging Paging) : IQuery
    {
        public static implicit operator ListCatalogsGridItems(ListCatalogsGridItemsRequest request)
            => new(request.Paging);
    }

    public record ListCatalogItemsListItems(Guid CatalogId, Paging Paging) : IQuery
    {
        public static implicit operator ListCatalogItemsListItems(ListCatalogItemsListItemsRequest request)
            => new(new(request.CatalogId), request.Paging);
    }

    public record ListCatalogItemsCards(Guid CatalogId, Paging Paging) : IQuery
    {
        public static implicit operator ListCatalogItemsCards(ListCatalogItemsCardsRequest request)
            => new(new(request.CatalogId), request.Paging);
    }

    public record GetCatalogItemDetails(Guid CatalogId, Guid ItemId) : IQuery
    {
        public static implicit operator GetCatalogItemDetails(GetCatalogItemDetailsRequest request)
            => new(new(request.CatalogId), new(request.ItemId));
    }
}