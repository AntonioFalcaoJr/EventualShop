using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;

namespace Application.UseCases.Events;

public class EmailChangedInteractor(IApplicationService service) : IInteractor<DomainEvent.EmailChanged>
{
    public Task InteractAsync(DomainEvent.EmailChanged @event, CancellationToken cancellationToken)
        => service.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(@event.UserId, @event.Email),
            cancellationToken: cancellationToken);
}