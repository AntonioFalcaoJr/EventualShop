using Application.Abstractions;
using Contracts.Boundaries.Account;

namespace Application.UseCases.Events;

public interface IProjectAccountDetailsWhenAccountChangedInteractor :
    IInteractor<DomainEvent.AccountCreated>,
    IInteractor<DomainEvent.AccountDeleted>,
    IInteractor<DomainEvent.AccountActivated>,
    IInteractor<DomainEvent.AccountDeactivated>;

public class ProjectAccountDetailsWhenAccountChangedInteractor(IProjectionGateway<Projection.AccountDetails> projectionGateway)
    : IProjectAccountDetailsWhenAccountChangedInteractor
{
    public async Task InteractAsync(DomainEvent.AccountCreated @event, CancellationToken cancellationToken)
    {
        Projection.AccountDetails accountDetails =
            new(@event.AccountId,
                @event.FirstName,
                @event.LastName,
                @event.Email,
                @event.Status,
                false,
                @event.Version);

        await projectionGateway.ReplaceInsertAsync(accountDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.AccountActivated @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: @event.AccountId,
            version: @event.Version,
            field: account => account.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.AccountDeactivated @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: @event.AccountId,
            version: @event.Version,
            field: account => account.Status,
            value: @event.Status,
            cancellationToken: cancellationToken);

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(@event.AccountId, cancellationToken);
}