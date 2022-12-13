using Contracts.Abstractions.Messages;
using Contracts.Services.Catalog.Protobuf;

namespace Contracts.Services.Catalog;

public static class Query
{
    public record GetCatalog(Guid CatalogId) : Message, IQuery
    {
        public static implicit operator GetCatalog(GetCatalogRequest request)
            => new(new Guid(request.Id));
    };

    public record GetCatalogs(ushort Limit, ushort Offset) : Message, IQuery
    {
        public static implicit operator GetCatalogs(ListCatalogsRequest request)
            => new((ushort)request.Limit, (ushort)request.Offset);
    }

    public record GetCatalogItems(Guid CatalogId, ushort Limit, ushort Offset) : Message, IQuery;

    public record GetAllItems(ushort Limit, ushort Offset) : Message, IQuery;
}