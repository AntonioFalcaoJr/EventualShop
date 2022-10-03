using Application.Abstractions.Interactors;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Identity;

namespace Infrastructure.MessageBus.Consumers.Events;

public class UserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public UserRegisteredConsumer(IInteractor<DomainEvent.UserRegistered> interactor) 
        : base(interactor) { }
}