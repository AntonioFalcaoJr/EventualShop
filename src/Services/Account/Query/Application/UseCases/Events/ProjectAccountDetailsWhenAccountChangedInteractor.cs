using Application.Abstractions;
using Contracts.Services.Account;

namespace Application.UseCases.Events;

public interface IProjectAccountDetailsWhenAccountChangedInteractor :
    IInteractor<DomainEvent.AccountCreated>,
    IInteractor<DomainEvent.AccountDeleted>,
    IInteractor<DomainEvent.AccountActivated>,
    IInteractor<DomainEvent.AccountDeactivated> { }

public class ProjectAccountDetailsWhenAccountChangedInteractor : IProjectAccountDetailsWhenAccountChangedInteractor
{
    private readonly IProjectionGateway<Projection.AccountDetails> _projectionGateway;

    public ProjectAccountDetailsWhenAccountChangedInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    {
        _projectionGateway = projectionGateway;
    }

    public async Task InteractAsync(DomainEvent.AccountCreated @event, CancellationToken cancellationToken)
    {
        Projection.AccountDetails accountDetails =
            new(@event.AccountId,
                @event.FirstName,
                @event.LastName,
                @event.Email,
                @event.Status,
                false);

        await _projectionGateway.UpsertAsync(accountDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.AccountActivated @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.AccountId,
            field: account => account.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
        => _projectionGateway.UpdateFieldAsync(
            id: @event.AccountId,
            field: account => account.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
        => _projectionGateway.DeleteAsync(@event.AccountId, cancellationToken);
}