using System;
using Messages.Abstractions;

namespace Messages.Catalogs
{
    public static class Queries
    {
        public record GetCatalogItemsDetailsWithPagination(Guid Id, int Limit, int Offset) : IQuery;
    }
}