using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RequestRebuildProjectionConsumer : Consumer<Command.RequestRebuildProjection>
{
    public RequestRebuildProjectionConsumer(IInteractor<Command.RequestRebuildProjection> interactor) 
        : base(interactor) { }
}
