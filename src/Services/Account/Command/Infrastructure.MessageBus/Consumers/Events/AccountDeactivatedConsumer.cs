using Application.Abstractions;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Account;

namespace Infrastructure.MessageBus.Consumers.Events;

public class AccountDeactivatedConsumer : Consumer<DomainEvent.AccountDeactivated>
{
    public AccountDeactivatedConsumer(IInteractor<DomainEvent.AccountDeactivated> interactor) 
        : base(interactor) { }
}