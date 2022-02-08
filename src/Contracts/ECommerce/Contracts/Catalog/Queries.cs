using System;
using ECommerce.Abstractions.Messages.Queries;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace ECommerce.Contracts.Catalog;

public static class Queries
{
    public record GetCatalogItemsDetails(Guid CatalogId, IPaging Paging) : QueryPaging(Paging);
}