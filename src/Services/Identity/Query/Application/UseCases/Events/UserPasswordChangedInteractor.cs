using Application.Abstractions;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserPasswordChangedInteractor : IInteractor<DomainEvent.PasswordChanged>
{
    private readonly IProjectionGateway<Projection.UserDetails> _gateway;

    public UserPasswordChangedInteractor(IProjectionGateway<Projection.UserDetails> gateway)
    {
        _gateway = gateway;
    }

    public Task InteractAsync(DomainEvent.PasswordChanged @event, CancellationToken cancellationToken)
        => _gateway.UpdateFieldAsync(
            id: @event.UserId,
            field: user => user.Password,
            value: @event.Password,
            cancellationToken: cancellationToken);
}