using Application.UseCases.Events;
using Infrastructure.MessageBus.Abstractions;
using Contracts.Services.Communication;

namespace Infrastructure.MessageBus.Consumers.Events;

public class SendNotificationWhenNotificationRequestedConsumer : Consumer<DomainEvent.NotificationRequested>
{
    public SendNotificationWhenNotificationRequestedConsumer(ISendNotificationWhenNotificationRequestedInteractor interactor)
        : base(interactor) { }
}