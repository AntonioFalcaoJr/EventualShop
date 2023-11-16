using Application.UseCases.Events;
using Contracts.Boundaries.Notification;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectNotificationDetailsWhenNotificationChangedConsumer(IProjectNotificationDetailsWhenNotificationChangedInteractor interactor)
    : Consumer<DomainEvent.NotificationRequested>(interactor);