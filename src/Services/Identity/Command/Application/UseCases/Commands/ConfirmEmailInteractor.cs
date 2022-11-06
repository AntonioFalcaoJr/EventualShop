using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public class ConfirmEmailInteractor : IInteractor<Command.ConfirmEmail>
{
    private readonly IApplicationService _service;

    public ConfirmEmailInteractor(IApplicationService service)
    {
        _service = service;
    }

    public async Task InteractAsync(Command.ConfirmEmail command, CancellationToken cancellationToken)
    {
        var aggregate = await _service.LoadAggregateAsync<User>(command.UserId, cancellationToken);
        aggregate.Handle(command);
        await _service.AppendEventsAsync(aggregate, cancellationToken);
    }
}