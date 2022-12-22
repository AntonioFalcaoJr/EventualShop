using Application.Abstractions.Interactors;
using Application.Resources;
using Application.Services;
using Contracts.DataTransferObjects;
using Contracts.Services.Identity;
using Domain.Aggregates;
using Command = Contracts.Services.Communication.Command;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered>
{
    private readonly IApplicationService _applicationService;

    public UserRegisteredInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
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