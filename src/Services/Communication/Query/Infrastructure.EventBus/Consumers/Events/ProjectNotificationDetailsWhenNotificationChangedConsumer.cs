using Application.UseCases.Events;
using Contracts.Services.Communication;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectNotificationDetailsWhenNotificationChangedConsumer : Consumer<DomainEvent.NotificationRequested>
{
    public ProjectNotificationDetailsWhenNotificationChangedConsumer(IProjectNotificationDetailsWhenNotificationChangedInteractor interactor)
        : base(interactor) { }
}