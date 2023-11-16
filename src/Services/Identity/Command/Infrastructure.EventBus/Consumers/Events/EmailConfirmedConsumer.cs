using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class EmailConfirmedConsumer(IInteractor<DomainEvent.EmailConfirmed> interactor) : Consumer<DomainEvent.EmailConfirmed>(interactor);