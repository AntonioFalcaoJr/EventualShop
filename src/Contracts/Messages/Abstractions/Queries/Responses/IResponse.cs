using MassTransit.Topology;

namespace Messages.Abstractions.Queries.Responses;

[ExcludeFromTopology]
public interface IResponse : IMessage { }