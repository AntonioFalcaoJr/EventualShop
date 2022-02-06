using MassTransit.Topology;

namespace ECommerce.Abstractions.Messages.Commands;

[ExcludeFromTopology]
public interface ICommand : IMessage { }