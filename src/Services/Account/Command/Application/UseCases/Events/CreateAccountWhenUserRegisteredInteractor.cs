using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;
using Domain.Aggregates;
using Command = Contracts.Boundaries.Account.Command;

namespace Application.UseCases.Events;

public interface ICreateAccountWhenUserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered> { }

public class CreateAccountWhenUserRegisteredInteractor(IApplicationService service) : ICreateAccountWhenUserRegisteredInteractor
{
    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Account account = new();

        var command = new Command.CreateAccount(
            @event.FirstName,
            @event.LastName,
            @event.Email);

        account.Handle(command);

        await service.AppendEventsAsync(account, cancellationToken);
    }
}