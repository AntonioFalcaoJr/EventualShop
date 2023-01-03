using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class UserPasswordChangedConsumer : Consumer<DomainEvent.UserPasswordChanged>
{
    public UserPasswordChangedConsumer(IInteractor<DomainEvent.UserPasswordChanged> interactor)
        : base(interactor) { }
}