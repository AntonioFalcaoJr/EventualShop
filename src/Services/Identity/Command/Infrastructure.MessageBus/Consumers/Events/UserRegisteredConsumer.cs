using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class UserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public UserRegisteredConsumer(IInteractor<DomainEvent.UserRegistered> interactor)
        : base(interactor) { }
}