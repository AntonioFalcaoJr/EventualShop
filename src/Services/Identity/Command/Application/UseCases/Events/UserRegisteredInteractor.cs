using Application.Abstractions;
using Application.Services;
using Contracts.Boundaries.Identity;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor(IApplicationService service) : IInteractor<DomainEvent.UserRegistered>
{
    public Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
        => service.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(@event.UserId, @event.Email),
            cancellationToken: cancellationToken);
}