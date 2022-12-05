using Application.Abstractions;
using Contracts.Services.Communication;

namespace Application.UseCases.Events;

public class EmailConfirmationRequestedInteractor : IInteractor<DomainEvent.EmailConfirmationRequested>
{
    private readonly IProjectionGateway<Projection.EmailSent> _projectionGateway;

    public EmailConfirmationRequestedInteractor(IProjectionGateway<Projection.EmailSent> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.EmailConfirmationRequested @event, CancellationToken cancellationToken)
    {
        Projection.EmailSent accountDetails = new(@event.Id, @event.UserId, @event.Email, false);
        await _projectionGateway.InsertAsync(accountDetails, cancellationToken);
    }
}