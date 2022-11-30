using Application.Abstractions;
using Application.Services;
using Domain.Aggregates;
using Contracts.Services.Account;
using Identity = Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor : IInteractor<Identity.DomainEvent.UserRegistered>
{
    private readonly IApplicationService _applicationService;

    public UserRegisteredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Identity.DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
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