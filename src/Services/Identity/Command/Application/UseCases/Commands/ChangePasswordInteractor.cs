using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ChangePasswordInteractor : IInteractor<Command.ChangePassword>
{
    private readonly IApplicationService _applicationService;

    public ChangePasswordInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.ChangePassword command, CancellationToken cancellationToken)
    {
        var user = await _applicationService.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        user.Handle(command);
        await _applicationService.AppendEventsAsync(user, cancellationToken);
    }
}