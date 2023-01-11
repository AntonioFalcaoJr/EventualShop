using Application.Abstractions;
using Application.Services;
using Contracts.Services.ShoppingCart;

namespace Application.UseCases.Commands;

public class RebuildProjectionInteractor : IInteractor<Command.RebuildProjection>
{
    private readonly IApplicationService _applicationService;

    public RebuildProjectionInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.RebuildProjection command, CancellationToken cancellationToken)
    {
        await foreach (var aggregateId in _applicationService.StreamAggregatesId(cancellationToken))
            await _applicationService.PublishEventAsync(new NotificationEvent.ProjectionRebuildRequested(aggregateId, command.Name), cancellationToken);
    }
}