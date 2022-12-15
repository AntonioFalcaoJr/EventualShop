using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog.Protobuf;

namespace Contracts.Services.Catalog;

public static class Query
{
    public record struct ListCatalogsGridItems(int? Limit, int? Offset) : IQuery
    {
        public static implicit operator ListCatalogsGridItems(ListCatalogsGridItemsRequest request)
            => new(request.Limit, request.Offset);
    }

    public record struct ListCatalogItemsListItems(Guid CatalogId, int? Limit, int? Offset) : IQuery
    {
        public static implicit operator ListCatalogItemsListItems(ListCatalogItemsListItemsRequest request)
            => new(new(request.CatalogId), request.Limit, request.Offset);
    }

    public record struct ListCatalogItemsCards(Guid CatalogId, int? Limit, int? Offset) : IQuery
    {
        public static implicit operator ListCatalogItemsCards(ListCatalogItemsCardsRequest request)
            => new(new(request.CatalogId), request.Limit, request.Offset);
    }

    public record struct GetCatalogItemDetails(Guid CatalogId, Guid ItemId) : IQuery
    {
        public static implicit operator GetCatalogItemDetails(GetCatalogItemDetailsRequest request)
            => new(new(request.CatalogId), new(request.ItemId));
    }
}