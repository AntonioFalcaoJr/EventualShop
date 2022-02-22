using System;
using ECommerce.Abstractions.Messages.Queries;

namespace ECommerce.Contracts.Catalog;

public static class Queries
{
    public record GetCatalogItemsDetails(Guid CatalogId, int Limit, int Offset) : QueryPaging(Limit, Offset, CorrelationId: CatalogId);
}