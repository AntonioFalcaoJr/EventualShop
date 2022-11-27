using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered>
{
    private readonly IApplicationService _applicationService;

    public UserRegisteredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
        => _applicationService.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(@event.UserId, @event.Email),
            cancellationToken: cancellationToken);
}