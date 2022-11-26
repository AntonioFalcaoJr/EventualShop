using Application.Abstractions;
using Contracts.Services.Identity;
using Infrastructure.MessageBus.Abstractions;

namespace Infrastructure.MessageBus.Consumers.Events;

public class EmailConfirmedConsumer : Consumer<DomainEvent.EmailConfirmed>
{
    public EmailConfirmedConsumer(IInteractor<DomainEvent.EmailConfirmed> interactor)
        : base(interactor) { }
}