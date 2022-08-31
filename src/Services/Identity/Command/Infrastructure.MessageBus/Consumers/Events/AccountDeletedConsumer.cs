using Application.Abstractions.Interactors;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Account;

namespace Infrastructure.MessageBus.Consumers.Events;

public class AccountDeletedConsumer : Consumer<DomainEvent.AccountDeleted>
{
    public AccountDeletedConsumer(IInteractor<DomainEvent.AccountDeleted> interactor)
        : base(interactor) { }
}