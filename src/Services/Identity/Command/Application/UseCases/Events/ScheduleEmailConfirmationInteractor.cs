using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class ScheduleEmailConfirmationInteractor :
    IInteractor<DomainEvent.UserRegistered>,
    IInteractor<DomainEvent.EmailChanged>
{
    private readonly IEventBusGateway _eventBusGateway;

    public ScheduleEmailConfirmationInteractor(IEventBusGateway eventBusGateway)
    {
        _eventBusGateway = eventBusGateway;
    }

    public Task InteractAsync(DomainEvent.EmailChanged @event, CancellationToken cancellationToken)
        => SchedulePublishAsync(@event.UserId, @event.Email, cancellationToken);

    public Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
        => SchedulePublishAsync(@event.UserId, @event.Email, cancellationToken);

    private Task SchedulePublishAsync(Guid aggregateId, string email, CancellationToken cancellationToken)
        => _eventBusGateway.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(aggregateId, email),
            cancellationToken: cancellationToken);
}