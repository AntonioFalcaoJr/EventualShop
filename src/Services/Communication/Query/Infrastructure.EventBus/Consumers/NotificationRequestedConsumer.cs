using Application.Abstractions;
using Contracts.Services.Communication;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class NotificationRequestedConsumer : Consumer<DomainEvent.NotificationRequested>
{
    public NotificationRequestedConsumer(IInteractor<DomainEvent.NotificationRequested> interactor)
        : base(interactor) { }
}