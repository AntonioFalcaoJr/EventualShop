using ECommerce.Abstractions;

namespace ECommerce.Contracts.Catalogs;

public static class Query
{
    public record GetCatalog(Guid CatalogId) : Message(CorrelationId: CatalogId), IQuery;

    public record GetCatalogs(int Limit, int Offset) : Message, IQuery;

    public record GetCatalogItems(Guid CatalogId, int Limit, int Offset) : Message(CorrelationId: CatalogId), IQuery;

    public record GetAllItems(int Limit, int Offset) : Message, IQuery;
}