using MassTransit;

namespace Contracts.Abstractions.Messages;

[ExcludeFromTopology]
public interface ICommand : IMessage { }

// TODO - Remove it after migration
[ExcludeFromTopology]
public interface ICommandWithId : ICommand
{
    Guid Id { get; }
}