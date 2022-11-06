using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers;

public class UserDeletedConsumer : Consumer<DomainEvent.UserDeleted>
{
    public UserDeletedConsumer(IInteractor<DomainEvent.UserDeleted> interactor)
        : base(interactor) { }
}