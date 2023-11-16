using Application.Abstractions;
using Contracts.Boundaries.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class AccountDeletedConsumer(IInteractor<DomainEvent.AccountDeleted> interactor) : Consumer<DomainEvent.AccountDeleted>(interactor);