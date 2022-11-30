using Application.Abstractions;
using Application.Services;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class EmailChangedInteractor : IInteractor<DomainEvent.EmailChanged>
{
    private readonly IApplicationService _applicationService;

    public EmailChangedInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public Task InteractAsync(DomainEvent.EmailChanged @event, CancellationToken cancellationToken)
        => _applicationService.SchedulePublishAsync(
            scheduledTime: DateTimeOffset.Now.AddMinutes(15),
            @event: new DelayedEvent.EmailConfirmationExpired(@event.UserId, @event.Email),
            cancellationToken: cancellationToken);
}