using MassTransit.Topology;
using Messages.Abstractions.Queries.Paging;

namespace Messages.Abstractions.Queries
{
    [ExcludeFromTopology]
    public record QueryPaging(int Limit, int Offset) : Query, IPaging;
}