using MassTransit.Topology;

namespace ECommerce.Abstractions.Queries;

[ExcludeFromTopology]
public interface IQuery : IMessage { }