using System.Collections.Generic;
using ECommerce.Abstractions.Messages.Queries.Paging;

namespace ECommerce.WebAPI.DataTransferObjects.ShoppingCarts;

public static class Outputs
{
    public record CartItemsPagedResult(IEnumerable<Dtos.Item> Items, PageInfo PageInfo) : IPagedResult<Dtos.Item>;
}