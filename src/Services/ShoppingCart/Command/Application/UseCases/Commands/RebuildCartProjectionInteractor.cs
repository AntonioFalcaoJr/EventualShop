using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Commands;

public class RebuildCartProjectionInteractor : IInteractor<Command.RebuildCartProjection>
{
    private readonly IApplicationService _applicationService;

    public RebuildCartProjectionInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.RebuildCartProjection command, CancellationToken cancellationToken)
    {
        await foreach (var cartId in _applicationService.StreamAggregatesId().WithCancellation(cancellationToken))
            await _applicationService.PublishEventAsync(new NotificationEvent.CartProjectionRebuildRequested(cartId, command.Projection), cancellationToken);
    }
}