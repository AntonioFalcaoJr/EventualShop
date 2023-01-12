using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectPaymentMethodListItemWhenCartChangedConsumer :
    IConsumer<DomainEvent.CreditCardAdded>,
    IConsumer<DomainEvent.DebitCardAdded>,
    IConsumer<DomainEvent.PayPalAdded>,
    IConsumer<DomainEvent.CartDiscarded>
{
    private readonly IProjectPaymentMethodListItemWhenCartChangedInteractor _interactor;

    public ProjectPaymentMethodListItemWhenCartChangedConsumer(IProjectPaymentMethodListItemWhenCartChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CreditCardAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.DebitCardAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.PayPalAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}