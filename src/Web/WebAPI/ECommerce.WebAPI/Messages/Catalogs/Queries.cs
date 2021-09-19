using System;
using Messages.Catalogs.Queries;

namespace ECommerce.WebAPI.Messages.Catalogs
{
    public static class Queries
    {
        public record GetCatalogItemsDetailsWithPaginationQuery(Guid Id, int Limit, int Offset) : GetCatalogItemsDetailsWithPagination;
    }
}