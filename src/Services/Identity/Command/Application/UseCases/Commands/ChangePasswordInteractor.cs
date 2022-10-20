using Application.Abstractions.Interactors;
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

    public async Task InteractAsync(Command.ChangePassword message, CancellationToken cancellationToken)
    {
        var aggregate = await _applicationService.LoadAggregateAsync<User>(message.Id, cancellationToken);
        aggregate.Handle(message);
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}