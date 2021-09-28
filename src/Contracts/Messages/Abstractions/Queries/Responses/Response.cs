using MassTransit.Topology;

namespace Messages.Abstractions.Queries.Responses
{
    [ExcludeFromTopology]
    public abstract record Response : Message, IResponse;
}