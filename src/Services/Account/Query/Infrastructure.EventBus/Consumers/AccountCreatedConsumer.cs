using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;
using MassTransit;

namespace Infrastructure.EventBus.Consumers;

public class AccountCreatedConsumer : Consumer<DomainEvent.AccountCreated>
{
    public AccountCreatedConsumer(IInteractor<DomainEvent.AccountCreated> interactor)
        : base(interactor) { }
}