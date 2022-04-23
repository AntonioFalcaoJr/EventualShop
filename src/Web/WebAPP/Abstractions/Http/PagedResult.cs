using ECommerce.Abstractions.Messages.Queries.Paging;

namespace WebAPP.Abstractions.Http;

public record PagedResult<T>(IEnumerable<T> Items, PageInfo PageInfo) : IPagedResult<T>;
