using MassTransit;

namespace Contracts.Abstractions;

[ExcludeFromTopology]
public interface IQuery : IMessage { }