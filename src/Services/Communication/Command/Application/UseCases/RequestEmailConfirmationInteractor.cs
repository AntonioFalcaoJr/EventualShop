using Application.Abstractions.Gateways;
using Application.Abstractions.Interactors;
using Application.Resources;
using Application.Services;
using Contracts.Services.Communication;
using Domain.Aggregates;
using DomainEvent = Contracts.Services.Identity.DomainEvent;

namespace Application.UseCases;

public class RequestEmailConfirmationInteractor : IInteractor<DomainEvent.UserRegistered>
{
    private readonly IApplicationService _applicationService;
    private readonly IEmailGateway _emailGateway;

    public RequestEmailConfirmationInteractor(
        IApplicationService applicationService, 
        IEmailGateway emailGateway)
    {
        _applicationService = applicationService;
        _emailGateway = emailGateway;
    }

    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        // TODO - Put de confirmation URL in some injectable Option
        var email = string.Format(EmailResource.EmailConfirmationHtml, @event.FirstName, $"http://localhost:5000/api/v1/identities/{@event.UserId}/confirm-email?Email={@event.Email}");
        await _emailGateway.SendHtmlEmailAsync(@event.Email, "Email Confirmation", email, cancellationToken);

        var aggregate = await _applicationService.LoadAggregateAsync<Notification>(@event.UserId, cancellationToken);
        aggregate.Handle(new Command.RequestEmailConfirmation(@event.UserId, @event.Email));
        await _applicationService.AppendEventsAsync(aggregate, cancellationToken);
    }
}