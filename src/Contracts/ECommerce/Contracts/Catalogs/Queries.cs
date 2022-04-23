using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Catalogs;

public static class Queries
{
    public record GetCatalog(Guid CatalogId) : Query;

    public record GetCatalogs(int Limit, int Offset) : QueryPaging(Limit, Offset);

    public record GetCatalogItems(Guid CatalogId, int Limit, int Offset) : QueryPaging(Limit, Offset, CorrelationId: CatalogId);
}