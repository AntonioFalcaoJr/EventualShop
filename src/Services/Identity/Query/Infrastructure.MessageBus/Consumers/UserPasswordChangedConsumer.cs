using Application.Abstractions.UseCases;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers;

public class UserPasswordChangedConsumer : Consumer<DomainEvent.UserPasswordChanged>
{
    public UserPasswordChangedConsumer(IInteractor<DomainEvent.UserPasswordChanged> interactor)
        : base(interactor) { }
}