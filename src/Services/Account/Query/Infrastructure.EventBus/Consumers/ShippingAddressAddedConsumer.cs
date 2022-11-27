using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class ShippingAddressAddedConsumer : Consumer<DomainEvent.ShippingAddressAdded>
{
    public ShippingAddressAddedConsumer(IInteractor<DomainEvent.ShippingAddressAdded> interactor)
        : base(interactor) { }
}