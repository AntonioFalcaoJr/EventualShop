using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using Infrastructure.EventBus.Abstractions;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentMethodListItemWhenCartChangedConsumer : Consumer<DomainEvent.PaymentMethodAdded>
{
    public ProjectPaymentMethodListItemWhenCartChangedConsumer(IProjectPaymentMethodListItemWhenCartChangedInteractor interactor)
        : base(interactor) { }
}