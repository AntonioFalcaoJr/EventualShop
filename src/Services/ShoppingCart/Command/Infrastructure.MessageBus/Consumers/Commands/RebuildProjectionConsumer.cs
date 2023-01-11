using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RebuildProjectionConsumer : Consumer<Command.RebuildProjection>
{
    public RebuildProjectionConsumer(IInteractor<Command.RebuildProjection> interactor)
        : base(interactor) { }
}