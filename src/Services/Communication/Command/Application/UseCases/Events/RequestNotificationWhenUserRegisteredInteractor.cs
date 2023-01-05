using Application.Abstractions;
using Application.Resources;
using Application.Services;
using Contracts.DataTransferObjects;
using Domain.Aggregates;
using Contracts.Services.Communication;
using Identity = Contracts.Services.Identity;

namespace Application.UseCases.Events;

public interface IRequestNotificationWhenUserRegisteredInteractor : IInteractor<Identity.DomainEvent.UserRegistered> { }

public class RequestNotificationWhenUserRegisteredInteractor : IRequestNotificationWhenUserRegisteredInteractor
{
    private readonly IApplicationService _applicationService;

    public RequestNotificationWhenUserRegisteredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Identity.DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Notification notification = new();
        var methods = DefineMethods(@event.UserId, @event.FirstName, @event.Email);
        var command = new Command.RequestNotification(methods);
        notification.Handle(command);
        await _applicationService.AppendEventsAsync(notification, cancellationToken);
    }

    private static IEnumerable<Dto.NotificationMethod> DefineMethods(Guid userId, string firstName, string address)
        => new[] { new Dto.NotificationMethod { Option = new Dto.Email(address, $"Welcome {firstName}!", FormatBody(userId, firstName, address)) } };

    private static string FormatBody(Guid userid, string firstname, string email)
        => string.Format(
            format: EmailResource.EmailConfirmationHtml,
            arg0: firstname,
            arg1: $"http://localhost:5000/api/v1/identities/{userid}/confirm-email?email={email}");
}