using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class AccountDeletedConsumer : Consumer<DomainEvent.AccountDeleted>
{
    public AccountDeletedConsumer(IInteractor<DomainEvent.AccountDeleted> interactor)
        : base(interactor) { }
}