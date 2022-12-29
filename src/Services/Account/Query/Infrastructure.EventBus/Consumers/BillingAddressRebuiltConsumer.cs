using Application.Abstractions;
using Application.UseCases.Events;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class BillingAddressRebuiltConsumer : Consumer<IntegrationEvent.ProjectionRebuilt>
{
    public BillingAddressRebuiltConsumer(BillingAddressRebuiltInteractor interactor) 
        : base(interactor) { }
}
