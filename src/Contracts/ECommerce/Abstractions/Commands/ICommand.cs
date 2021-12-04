using MassTransit.Topology;

namespace ECommerce.Abstractions.Commands;

[ExcludeFromTopology]
public interface ICommand : IMessage { }