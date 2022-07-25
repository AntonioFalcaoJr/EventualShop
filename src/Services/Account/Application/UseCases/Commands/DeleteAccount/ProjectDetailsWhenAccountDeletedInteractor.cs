using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.DeleteAccount;

public class ProjectDetailsWhenAccountDeletedInteractor : IInteractor<DomainEvent.AccountDeleted>
{
    private readonly IProjectionRepository<Projection.AccountDetails> _repository;

    public ProjectDetailsWhenAccountDeletedInteractor(IProjectionRepository<Projection.AccountDetails> repository)
    {
        _repository = repository;
    }

    public Task InteractAsync(DomainEvent.AccountDeleted @event, CancellationToken ct)
        => _repository.DeleteAsync(@event.AccountId, ct);
}