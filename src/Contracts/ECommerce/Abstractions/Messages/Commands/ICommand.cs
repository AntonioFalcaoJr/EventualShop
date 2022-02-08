using MassTransit;

namespace ECommerce.Abstractions.Messages.Commands;

[ExcludeFromTopology]
public interface ICommand : IMessage { }