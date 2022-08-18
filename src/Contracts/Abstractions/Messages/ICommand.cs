using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface ICommand : IMessage { }

public interface ICommandWithId : ICommand
{
    Guid Id { get; }
}