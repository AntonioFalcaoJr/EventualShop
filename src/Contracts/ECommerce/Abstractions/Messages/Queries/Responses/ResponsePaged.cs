using ECommerce.Abstractions.Messages.Queries.Paging;
using MassTransit;

namespace ECommerce.Abstractions.Messages.Queries.Responses;

[ExcludeFromTopology]
public abstract record ResponsePaged<T> : Message, IResponse, IPagedResult<T>
{
    public IEnumerable<T> Items { get; init; }
    public PageInfo PageInfo { get; init; }
}