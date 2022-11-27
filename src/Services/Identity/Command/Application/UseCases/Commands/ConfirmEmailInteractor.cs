using Application.Abstractions;
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

    public async Task InteractAsync(Command.ConfirmEmail command, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        aggregate.Handle(command);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}