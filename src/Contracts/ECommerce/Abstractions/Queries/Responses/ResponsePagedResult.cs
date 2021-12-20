using ECommerce.Abstractions.Queries.Paging;
using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries.Responses;

[ExcludeFromTopology]
public abstract record ResponsePagedResult<T> : Message, IResponse, IPagedResult<T>
{
    public IEnumerable<T> Items { get; init; }
    public IPageInfo PageInfo { get; init; }
}