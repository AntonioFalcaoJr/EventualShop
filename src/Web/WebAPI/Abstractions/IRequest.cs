using Contracts.Abstractions.Messages;
using Grpc.Core;
using MassTransit;

namespace WebAPI.Abstractions;

public interface IRequest
{
    CancellationToken CancellationToken { get; }
    bool IsValid(out IDictionary<string, string[]> errors);
}

public interface ICommand<out TCommand> : IRequest
    where TCommand : ICommand
{
    TCommand Command { get; }
    IBus Bus { get; }
}

public interface IQuery<out TClient> : IRequest
    where TClient : ClientBase
{
    TClient Client { get; }
}