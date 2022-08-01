using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Identity;

namespace Application.UseCases.Events;

public class UserRegisteredInteractor : IInteractor<DomainEvent.UserRegistered>
{
    private readonly IProjectionRepository<Projection.UserDetails> _repository;

    public UserRegisteredInteractor(IProjectionRepository<Projection.UserDetails> repository)
    {
        _repository = repository;
    }

    public Task InteractAsync(DomainEvent.UserRegistered @event, CancellationToken ct)
    {
        Projection.UserDetails userDetails = new(
            @event.UserId,
            @event.FirstName,
            @event.LastName,
            @event.Email,
            @event.Password,
            default,
            false);

        return _repository.InsertAsync(userDetails, ct);
    }
}