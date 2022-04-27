using MassTransit;

namespace Contracts.Abstractions;

[ExcludeFromTopology]
public interface ICommand : IMessage { }