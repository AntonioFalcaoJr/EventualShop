using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;
using Domain.Aggregates;
using Command = Contracts.Services.Account.Command;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered>
{
    private readonly IApplicationService _applicationService;

    public UserRegisteredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Account account = new();

        var command = new Command.CreateAccount(
            @event.FirstName,
            @event.LastName,
            @event.Email);

        account.Handle(command);

        await _applicationService.AppendEventsAsync(account, cancellationToken);
    }
}