using System;
using ECommerce.Abstractions.Queries;

namespace ECommerce.Contracts.Catalog;

public static class Queries
{
    public record GetCatalogItemsDetailsWithPagination(Guid CatalogId, int Limit, int Offset) : QueryPaging(Limit, Offset);
}