using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface ICommand : IMessage { }

// TODO - Remove it after migration
public interface ICommandWithId : ICommand
{
    Guid Id { get; }
}