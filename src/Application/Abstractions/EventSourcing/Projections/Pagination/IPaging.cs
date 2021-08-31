using MassTransit.Topology;

namespace Application.Abstractions.EventSourcing.Projections.Pagination
{
    [ExcludeFromTopology]
    public interface IPaging
    {
        int Limit { get; }
        int Offset { get; }
    }
}