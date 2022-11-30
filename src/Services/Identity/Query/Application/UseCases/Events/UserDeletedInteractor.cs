using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserDeletedInteractor : IInteractor<DomainEvent.UserDeleted>
{
    private readonly IProjectionGateway<Projection.UserDetails> _gateway;

    public UserDeletedInteractor(IProjectionGateway<Projection.UserDetails> gateway)
    {
        _gateway = gateway;
    }

    public Task InteractAsync(DomainEvent.UserDeleted @event, CancellationToken cancellationToken)
        => _gateway.DeleteAsync(@event.UserId, cancellationToken);
}