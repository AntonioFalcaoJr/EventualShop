using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface ICommand : IMessage { }