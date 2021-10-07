using System.Collections.Generic;
using MassTransit.Topology;
using Messages.Abstractions.Queries.Paging;

namespace Messages.Abstractions.Queries.Responses
{
    [ExcludeFromTopology]
    public abstract record ResponsePagedResult<T> : Message, IResponse, IPagedResult<T>
    {
        public IEnumerable<T> Items { get; init; }
        public IPageInfo PageInfo { get; init; }
    }
}