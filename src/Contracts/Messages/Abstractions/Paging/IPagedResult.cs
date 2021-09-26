using System.Collections.Generic;
using MassTransit.Topology;

namespace Messages.Abstractions.Paging
{
    [ExcludeFromTopology]
    public interface IPagedResult<out T>
    {
        IEnumerable<T> Items { get; }
        IPageInfo PageInfo { get; }
    }
}