using System;
using System.Collections.Generic;
using Messages.Abstractions.Queries.Paging;
using Messages.Abstractions.Queries.Responses;

namespace Messages.Catalogs
{
    public static class Responses
    {
        public record CatalogItemsDetails(Guid CatalogId, string Name, string Description, decimal Price, string PictureUri);

        public record CatalogItemsDetailsPagedResult(IEnumerable<CatalogItemsDetails> Items, IPageInfo PageInfo)
            : ResponsePagedResult<CatalogItemsDetails>(Items, PageInfo);
    }
}