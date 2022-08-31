using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserPasswordChangedInteractor : IInteractor<DomainEvent.UserPasswordChanged>
{
    private readonly IProjectionRepository<Projection.UserDetails> _repository;

    public UserPasswordChangedInteractor(IProjectionRepository<Projection.UserDetails> repository)
    {
        _repository = repository;
    }

    public Task InteractAsync(DomainEvent.UserPasswordChanged @event, CancellationToken ct)
        => _repository.UpdateFieldAsync(
            id: @event.UserId,
            field: user => user.Password,
            value: @event.NewPassword,
            cancellationToken: ct);
}