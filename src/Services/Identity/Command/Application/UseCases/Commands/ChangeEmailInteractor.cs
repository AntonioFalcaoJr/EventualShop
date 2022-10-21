using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ChangeEmailInteractor : IInteractor<Command.ChangeEmail>
{
    private readonly IApplicationService _applicationService;

    public ChangeEmailInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.ChangeEmail message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.UserId, cancellationToken);
        aggregate.Handle(message);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}