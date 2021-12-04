using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries.Paging;

[ExcludeFromTopology]
public interface IPaging
{
    int Limit { get; }
    int Offset { get; }
}