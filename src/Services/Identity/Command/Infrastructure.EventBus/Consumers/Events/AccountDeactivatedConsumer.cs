using Application.Abstractions;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class AccountDeactivatedConsumer(IInteractor<DomainEvent.AccountDeactivated> interactor) 
    : Consumer<DomainEvent.AccountDeactivated>(interactor);