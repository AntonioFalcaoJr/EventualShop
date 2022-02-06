using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Queries;

[ExcludeFromTopology]
public interface IQuery : IMessage { }