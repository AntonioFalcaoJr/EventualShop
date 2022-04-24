using MassTransit;

namespace ECommerce.Abstractions.Paging;

[ExcludeFromTopology]
public interface IPagedResult<out T>
{
    IEnumerable<T> Items { get; }
    PageInfo PageInfo { get; }
}