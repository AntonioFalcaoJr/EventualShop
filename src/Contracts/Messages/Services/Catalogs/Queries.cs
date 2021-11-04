using System;
using Messages.Abstractions.Queries;

namespace Messages.Services.Catalogs;

public static class Queries
{
    public record GetCatalogItemsDetailsWithPagination(Guid CatalogId, int Limit, int Offset) : QueryPaging(Limit, Offset);
}