using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.CreateAccount;

public class ProjectDetailsWhenAccountCreatedInteractor : IInteractor<DomainEvent.AccountCreated>
{
    private readonly IProjectionRepository<Projection.AccountDetails> _repository;

    public ProjectDetailsWhenAccountCreatedInteractor(IProjectionRepository<Projection.AccountDetails> repository)
    {
        _repository = repository;
    }

    public Task InteractAsync(DomainEvent.AccountCreated @event, CancellationToken ct)
        => _repository.InsertAsync(new(@event.Id, false), ct);
}