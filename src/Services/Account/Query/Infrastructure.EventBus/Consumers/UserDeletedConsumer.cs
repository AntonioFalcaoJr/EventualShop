using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class UserDeletedConsumer : Consumer<DomainEvent.UserDeleted>
{
    public UserDeletedConsumer(IInteractor<DomainEvent.UserDeleted> interactor)
        : base(interactor) { }
}