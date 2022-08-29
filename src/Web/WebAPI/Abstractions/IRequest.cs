using Contracts.Abstractions.Messages;
using MassTransit;

namespace WebAPI.Abstractions;

public interface IRequest
{
    IBus Bus { get; }
    CancellationToken CancellationToken { get; }
    bool IsValid(out IDictionary<string, string[]> errors);
    ICommand AsCommand();
}