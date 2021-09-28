using MassTransit.Topology;

namespace Messages.Abstractions.Queries.Paging
{
    [ExcludeFromTopology]
    public interface IPaging
    {
        int Limit { get; }
        int Offset { get; }
    }
}