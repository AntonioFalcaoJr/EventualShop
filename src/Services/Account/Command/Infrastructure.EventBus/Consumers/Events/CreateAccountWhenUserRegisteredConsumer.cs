using Application.UseCases.Events;
using Contracts.Boundaries.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class CreateAccountWhenUserRegisteredConsumer(ICreateAccountWhenUserRegisteredInteractor interactor) : Consumer<DomainEvent.UserRegistered>(interactor);