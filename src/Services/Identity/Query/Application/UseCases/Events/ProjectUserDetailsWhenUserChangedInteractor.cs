using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public interface IProjectUserDetailsWhenUserChangedInteractor : 
    IInteractor<DomainEvent.UserDeleted>,
    IInteractor<DomainEvent.UserRegistered>,
    IInteractor<DomainEvent.UserPasswordChanged> { }

public class ProjectUserDetailsWhenUserChangedInteractor : IProjectUserDetailsWhenUserChangedInteractor
{
    private readonly IProjectionGateway<Projection.UserDetails> _gateway;

    public ProjectUserDetailsWhenUserChangedInteractor(IProjectionGateway<Projection.UserDetails> gateway)
    {
        _gateway = gateway;
    }

    public Task InteractAsync(DomainEvent.UserDeleted @event, CancellationToken cancellationToken)
        => _gateway.DeleteAsync(@event.UserId, cancellationToken);

    public Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken cancellationToken)
    {
        Projection.UserDetails userDetails = 
            new(@event.UserId, @event.FirstName, @event.LastName, @event.Email, @event.Password, false);

        return _gateway.InsertAsync(userDetails, cancellationToken);
    }

    public Task InteractAsync(DomainEvent.UserPasswordChanged @event, CancellationToken cancellationToken)
        => _gateway.UpdateFieldAsync(
            id: @event.UserId,
            field: user => user.Password,
            value: @event.Password,
            cancellationToken: cancellationToken);
}