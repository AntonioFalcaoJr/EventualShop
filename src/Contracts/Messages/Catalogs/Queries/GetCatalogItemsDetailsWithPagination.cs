using System;
using Messages.Paging;

namespace Messages.Catalogs.Queries
{
    public interface GetCatalogItemsDetailsWithPagination : IPaging
    {
        Guid Id { get; }
    }
}