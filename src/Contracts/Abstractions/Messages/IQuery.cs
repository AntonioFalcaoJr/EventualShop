using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface IQuery : IMessage { }