using Contracts.Abstractions.Paging;

namespace WebAPP.Abstractions.Http;

public record PagedResult<T>(IReadOnlyList<T> Items, Page Page) : IPagedResult<T>;