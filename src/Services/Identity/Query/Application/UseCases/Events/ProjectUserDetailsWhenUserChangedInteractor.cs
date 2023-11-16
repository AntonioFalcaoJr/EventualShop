using Application.Abstractions;
using Contracts.Boundaries.Identity;

namespace Application.UseCases.Events;

public interface IProjectUserDetailsWhenUserChangedInteractor :
    IInteractor<DomainEvent.UserDeleted>,
    IInteractor<DomainEvent.UserRegistered>,
    IInteractor<DomainEvent.UserPasswordChanged>;

public class ProjectUserDetailsWhenUserChangedInteractor(IProjectionGateway<Projection.UserDetails> projectionGateway)
    : IProjectUserDetailsWhenUserChangedInteractor
{
    public Task InteractAsync(DomainEvent.UserDeleted @event, CancellationToken cancellationToken)
        => projectionGateway.DeleteAsync(@event.UserId, cancellationToken);

    public async Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Projection.UserDetails userDetails = new(
            @event.UserId,
            @event.FirstName,
            @event.LastName,
            @event.Email,
            @event.Password,
            false,
            @event.Version);

        await projectionGateway.ReplaceInsertAsync(userDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.UserPasswordChanged @event, CancellationToken cancellationToken)
        => projectionGateway.UpdateFieldAsync(
            id: @event.UserId,
            version: @event.Version,
            field: user => user.Password,
            value: @event.Password,
            cancellationToken: cancellationToken);
}