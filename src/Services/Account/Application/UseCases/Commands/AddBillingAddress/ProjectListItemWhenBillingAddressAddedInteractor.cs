using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.AddBillingAddress;

public class ProjectListItemWhenBillingAddressAddedInteractor : IInteractor<DomainEvent.BillingAddressAdded>
{
    private readonly IProjectionRepository<Projection.AddressListItem> _repository;

    public ProjectListItemWhenBillingAddressAddedInteractor(IProjectionRepository<Projection.AddressListItem> repository)
    {
        _repository = repository;
    }

    public async Task InteractAsync(DomainEvent.BillingAddressAdded @event, CancellationToken ct)
    {
        Projection.BillingAddressListItem addressListItem = new(@event.AddressId, @event.AccountId, @event.Address, false);
        await _repository.InsertAsync(addressListItem, ct);
    }
}