using System;
using Messages.Catalogs.Queries;

namespace ECommerce.WebAPI.Messages.Catalogs
{
    public static class Queries
    {
        public record GetCatalogItemsDetailsWithPaginationWithPaginationQuery(Guid Id, int Limit, int Offset) : GetCatalogItemsDetailsWithPagination;
    }
}