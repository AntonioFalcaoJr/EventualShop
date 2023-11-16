using Application.Abstractions;
using Application.Resources;
using Application.Services;
using Contracts.Boundaries.Identity;
using Contracts.DataTransferObjects;
using Domain.Aggregates;
using Command = Contracts.Boundaries.Notification.Command;

namespace Application.UseCases.Events;

public interface IRequestNotificationWhenUserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered>;

public class RequestNotificationWhenUserRegisteredInteractor(IApplicationService service) : IRequestNotificationWhenUserRegisteredInteractor
{
    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Notification notification = new();
        var methods = DefineMethods(@event.UserId, @event.FirstName, @event.Email);
        var command = new Command.RequestNotification(methods);
        notification.Handle(command);
        await service.AppendEventsAsync(notification, cancellationToken);
    }

    private static IEnumerable<Dto.NotificationMethod> DefineMethods(Guid userId, string firstName, string address)
        => new[] { new Dto.NotificationMethod(Guid.NewGuid(), new Dto.Email(address, $"Welcome {firstName}!", FormatBody(userId, firstName, address))) };

    private static string FormatBody(Guid userid, string firstname, string email)
        => string.Format(
            format: EmailResource.EmailConfirmationHtml,
            arg0: firstname,
            arg1: $"http://localhost:5000/api/v1/identities/{userid}/confirm-email?email={email}");
}