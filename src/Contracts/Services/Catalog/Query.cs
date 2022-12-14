using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog.Protobuf;

namespace Contracts.Services.Catalog;

public static class Query
{
    public record struct ListCatalogsGridItems(ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListCatalogsGridItems(ListCatalogsGridItemsRequest request)
            => new((ushort)request.Limit, (ushort)request.Offset);
    }

    public record struct ListCatalogItemsListItems(Guid CatalogId, ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListCatalogItemsListItems(ListCatalogItemsListItemsRequest request)
            => new(new(request.CatalogId), (ushort)request.Limit, (ushort)request.Offset);
    }

    public record struct ListCatalogItemsCards(Guid CatalogId, ushort Limit, ushort Offset) : IQuery
    {
        public static implicit operator ListCatalogItemsCards(ListCatalogItemsCardsRequest request)
            => new(new(request.CatalogId), (ushort)request.Limit, (ushort)request.Offset);
    }

    public record struct GetCatalogItemDetails(Guid CatalogId, Guid ItemId) : IQuery
    {
        public static implicit operator GetCatalogItemDetails(GetCatalogItemDetailsRequest request)
            => new(new(request.CatalogId), new(request.ItemId));
    }
}