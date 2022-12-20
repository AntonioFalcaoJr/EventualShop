using Application.Abstractions;
using Application.Services;
using Contracts.Services.Account;

namespace Application.UseCases.Commands;

public class RequestRebuildProjectionInteractor : IInteractor<Command.RequestRebuildProjection>
{
    private readonly IApplicationService _applicationService;

    public RequestRebuildProjectionInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task InteractAsync(Command.RequestRebuildProjection message, CancellationToken cancellationToken)
        => _applicationService.StreamReplayAsync(message.Name, cancellationToken);
}