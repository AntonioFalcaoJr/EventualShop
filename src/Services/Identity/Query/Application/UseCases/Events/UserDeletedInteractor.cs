using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserDeletedInteractor : IInteractor<DomainEvent.UserDeleted>
{
    private readonly IProjectionRepository<Projection.UserDetails> _repository;

    public UserDeletedInteractor(IProjectionRepository<Projection.UserDetails> repository)
    {
        _repository = repository;
    }

    public Task InteractAsync(DomainEvent.UserDeleted @event, CancellationToken ct)
        => _repository.DeleteAsync(@event.UserId, ct);
}