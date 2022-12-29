using Contracts.Abstractions.Paging;
using Google.Protobuf;
using PagedResult = Contracts.Abstractions.Protobuf.PagedResult;

namespace WebAPI.Abstractions;

public record struct PagedResult<T> : IPagedResult<T>
    where T : IMessage, new()
{
    public IEnumerable<T> Items { get; init; }
    public Page Page { get; init; }

    public static implicit operator PagedResult<T>(PagedResult paged)
        => new() { Items = paged.Projections.Select(x => x.Unpack<T>()), Page = paged.Page };
}