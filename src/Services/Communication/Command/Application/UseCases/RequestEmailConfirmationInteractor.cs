using Application.Abstractions;
using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Resources;
using Contracts.Services.Communication;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Identity.DomainEvent;

namespace Application.UseCases;

public class RequestEmailConfirmationInteractor : EventInteractor<Notification, DomainEvent.UserRegistered>
{
    private readonly IEmailGateway _emailGateway;

    public RequestEmailConfirmationInteractor(
        IEventStoreGateway eventStoreGateway, 
        IEventBusGateway eventBusGateway, 
        IEmailGateway emailGateway, 
        IUnitOfWork unitOfWork)
        : base(eventStoreGateway, eventBusGateway, unitOfWork) 
        => _emailGateway = emailGateway;

    public override async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        // TODO - Put de confirmation URL in some injectable Option
        var email = string.Format(EmailResource.EmailConfirmationHtml, @event.FirstName, $"http://localhost:5000/api/v1/identities/{@event.Id}/confirm-email?Email={@event.Email}");
        await _emailGateway.SendHtmlEmailAsync(@event.Email, "Email Confirmation", email, cancellationToken);
        await OnInteractAsync(@event.Id, _ => new Command.RequestEmailConfirmation(@event.Id, @event.Email), cancellationToken);
    }
}