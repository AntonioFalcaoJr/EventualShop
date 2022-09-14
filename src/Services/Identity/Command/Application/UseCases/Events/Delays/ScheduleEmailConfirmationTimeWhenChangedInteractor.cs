using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Contracts.Services.Identity;
using Domain.Aggregates;

namespace Application.UseCases.Events.Delays;

public class ScheduleEmailConfirmationTimeWhenChangedInteractor : EventInteractor<User, DomainEvent.EmailChanged>
{
    public ScheduleEmailConfirmationTimeWhenChangedInteractor(IEventStoreGateway eventStoreGateway, IEventBusGateway eventBusGateway, IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) { }

    public override Task InteractAsync(DomainEvent.EmailChanged @event, CancellationToken cancellationToken)
        => EventBusGateway.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(@event.Id, @event.Email),
            cancellationToken: cancellationToken);
}