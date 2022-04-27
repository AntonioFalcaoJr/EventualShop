using MassTransit;

namespace Contracts.Abstractions.Paging;

[ExcludeFromTopology]
public interface IPagedResult<out T>
{
    IEnumerable<T> Items { get; }
    PageInfo PageInfo { get; }
}