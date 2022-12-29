using Application.UseCases.Events;
using Contracts.Services.Account;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers;

public class ShippingAddressRebuiltConsumer : Consumer<IntegrationEvent.ProjectionRebuilt>
{
    public ShippingAddressRebuiltConsumer(ShippingAddressRebuiltInteractor interactor) 
        : base(interactor) { }
}
