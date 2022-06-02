using Contracts.Abstractions.Paging;

namespace WebAPP.Abstractions.Http;

public record PagedResult<T>(IEnumerable<T> Items, Page Page) : IPagedResult<T>;
