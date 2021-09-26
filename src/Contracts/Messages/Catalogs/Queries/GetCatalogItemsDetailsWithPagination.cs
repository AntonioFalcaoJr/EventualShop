using System;
using Messages.Abstractions.Paging;

namespace Messages.Catalogs.Queries
{
    public interface GetCatalogItemsDetailsWithPagination : IPaging
    {
        Guid Id { get; }
    }
}