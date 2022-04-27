using Contracts.Abstractions.Paging;

namespace WebAPP.Abstractions.Http;

public record PagedResult<T>(IEnumerable<T> Items, PageInfo PageInfo) : IPagedResult<T>;
