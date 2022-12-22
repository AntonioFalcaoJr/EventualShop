using Application.Abstractions.Interactors;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Communication;

namespace Infrastructure.MessageBus.Consumers.Events;

public class NotificationRequestedConsumer : Consumer<DomainEvent.NotificationRequested>
{
    public NotificationRequestedConsumer(IInteractor<DomainEvent.NotificationRequested> interactor) 
        : base(interactor) { }
}