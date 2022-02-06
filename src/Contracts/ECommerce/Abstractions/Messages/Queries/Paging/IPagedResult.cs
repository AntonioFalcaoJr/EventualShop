using System.Collections.Generic;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Queries.Paging;

[ExcludeFromTopology]
public interface IPagedResult<out T>
{
    IEnumerable<T> Items { get; }
    IPageInfo PageInfo { get; }
}