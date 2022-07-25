using Application.Abstractions.Projections;
using Application.Abstractions.UseCases;
using Contracts.Services.Account;

namespace Application.UseCases.Commands.AddShippingAddress;

public class ProjectListItemWhenShippingAddressAddedInteractor : IInteractor<DomainEvent.ShippingAddressAdded>
{
    private readonly IProjectionRepository<Projection.AddressListItem> _repository;

    public ProjectListItemWhenShippingAddressAddedInteractor(IProjectionRepository<Projection.AddressListItem> repository)
    {
        _repository = repository;
    }

    public async Task InteractAsync(DomainEvent.ShippingAddressAdded @event, CancellationToken ct)
    {
        Projection.ShippingAddressListItem addressListItem = new(@event.AddressId, @event.AccountId, @event.Address, false);
        await _repository.InsertAsync(addressListItem, ct);
    }
}