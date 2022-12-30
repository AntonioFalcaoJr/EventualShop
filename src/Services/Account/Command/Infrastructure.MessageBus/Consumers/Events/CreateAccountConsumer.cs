using Application.UseCases.Events;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class CreateAccountConsumer : Consumer<DomainEvent.UserRegistered>
{
    public CreateAccountConsumer(ICreateAccountInteractor interactor)
        : base(interactor) { }
}