using System.Collections.Generic;
using MassTransit.Topology;
using Messages.Abstractions.Queries.Paging;

namespace Messages.Abstractions.Queries.Responses
{
    [ExcludeFromTopology]
    public abstract record ResponsePagedResult<T>(IEnumerable<T> Items, IPageInfo PageInfo) : Message, IResponse, IPagedResult<T>;
}