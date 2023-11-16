using Application.UseCases.Events;
using Contracts.Boundaries.Shopping.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentMethodListItemWhenCartChangedConsumer(IProjectPaymentMethodListItemWhenCartChangedInteractor interactor)
    :
        // IConsumer<DomainEvent.CreditCardAdded>,
        // IConsumer<DomainEvent.DebitCardAdded>,
        // IConsumer<DomainEvent.PayPalAdded>,
        IConsumer<DomainEvent.CartDiscarded>
{
    // public Task Consume(ConsumeContext<DomainEvent.CreditCardAdded> context)
    //     => interactor.InteractAsync(context.Message, context.CancellationToken);
    //
    // public Task Consume(ConsumeContext<DomainEvent.DebitCardAdded> context)
    //     => interactor.InteractAsync(context.Message, context.CancellationToken);
    //
    // public Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
    //     => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}