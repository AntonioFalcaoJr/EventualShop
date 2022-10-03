using Application.Abstractions.UseCases;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers;

public class UserPasswordChangedConsumer : Consumer<DomainEvent.PasswordChanged>
{
    public UserPasswordChangedConsumer(IInteractor<DomainEvent.PasswordChanged> interactor)
        : base(interactor) { }
}