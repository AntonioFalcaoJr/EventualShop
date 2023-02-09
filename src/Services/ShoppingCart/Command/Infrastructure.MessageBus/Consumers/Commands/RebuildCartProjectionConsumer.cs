using Application.Abstractions;
using Contracts.Services.ShoppingCart;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Commands;

public class RebuildCartProjectionConsumer : Consumer<Command.RebuildCartProjection>
{
    public RebuildCartProjectionConsumer(IInteractor<Command.RebuildCartProjection> interactor)
        : base(interactor) { }
}