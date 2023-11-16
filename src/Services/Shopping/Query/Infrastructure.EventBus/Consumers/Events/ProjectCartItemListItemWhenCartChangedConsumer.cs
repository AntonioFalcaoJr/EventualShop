using Application.UseCases.Events;
using Contracts.Boundaries.Shopping.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCartItemListItemWhenCartChangedConsumer(IProjectCartItemListItemWhenCartChangedInteractor interactor)
    :
        IConsumer<DomainEvent.CartItemAdded>,
        IConsumer<DomainEvent.CartItemRemoved>,
        IConsumer<DomainEvent.CartItemIncreased>,
        IConsumer<DomainEvent.CartDiscarded>,
        IConsumer<DomainEvent.CartItemDecreased>
{
    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}