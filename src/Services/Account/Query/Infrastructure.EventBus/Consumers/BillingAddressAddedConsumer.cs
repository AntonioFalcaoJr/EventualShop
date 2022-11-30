using Application.Abstractions;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class BillingAddressAddedConsumer : Consumer<DomainEvent.BillingAddressAdded>
{
    public BillingAddressAddedConsumer(IInteractor<DomainEvent.BillingAddressAdded> interactor)
        : base(interactor) { }
}