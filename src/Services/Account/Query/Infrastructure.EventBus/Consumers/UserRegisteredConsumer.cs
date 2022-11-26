using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class UserRegisteredConsumer : Consumer<DomainEvent.UserRegistered>
{
    public UserRegisteredConsumer(IInteractor<DomainEvent.UserRegistered> interactor)
        : base(interactor) { }
}