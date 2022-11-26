using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class UserPasswordChangedConsumer : Consumer<DomainEvent.PasswordChanged>
{
    public UserPasswordChangedConsumer(IInteractor<DomainEvent.PasswordChanged> interactor)
        : base(interactor) { }
}