using Application.UseCases.Events;
using Contracts.Services.Identity;
using MessageBus.Abstractions;

namespace MessageBus.Consumers.Events;

public class CreateAccountWhenUserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public CreateAccountWhenUserRegisteredConsumer(ICreateAccountWhenUserRegisteredInteractor interactor)
        : base(interactor) { }
}