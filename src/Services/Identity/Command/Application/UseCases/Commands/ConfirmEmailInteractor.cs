using Application.Abstractions.Interactors;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ConfirmEmailInteractor : IInteractor<Command.ConfirmEmail>
{
    private readonly IApplicationService _applicationService;

    public ConfirmEmailInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.ConfirmEmail message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.Id, cancellationToken);
        aggregate.Handle(message);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}