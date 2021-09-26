using MassTransit.Topology;

namespace Messages.Abstractions.Paging
{
    [ExcludeFromTopology]
    public interface IPaging
    {
        int Limit { get; }
        int Offset { get; }
    }
}