using Application.UseCases.Events;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class CreateAccountWhenUserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public CreateAccountWhenUserRegisteredConsumer(ICreateAccountWhenUserRegisteredInteractor interactor)
        : base(interactor) { }
}