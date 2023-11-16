using Application.Abstractions;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class UserRegisteredConsumer(IInteractor<DomainEvent.UserRegistered> interactor) : Consumer<DomainEvent.UserRegistered>(interactor);