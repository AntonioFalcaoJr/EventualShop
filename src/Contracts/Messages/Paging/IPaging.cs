using MassTransit.Topology;

namespace Messages.Paging
{
    [ExcludeFromTopology]
    public interface IPaging
    {
        int Limit { get; }
        int Offset { get; }
    }
}