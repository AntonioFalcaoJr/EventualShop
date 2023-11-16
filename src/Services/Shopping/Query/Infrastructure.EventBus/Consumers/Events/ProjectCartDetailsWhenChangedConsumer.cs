using Application.UseCases.Events;
using Contracts.Boundaries.Shopping.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCartDetailsWhenChangedConsumer(IProjectCartDetailsWhenCartChangedInteractor interactor) :
    IConsumer<DomainEvent.ShoppingStarted>,
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartCheckedOut>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartItemDecreased>,
    IConsumer<DomainEvent.CartDiscarded>,
    IConsumer<SummaryEvent.CartProjectionRebuilt>
{
    public Task Consume(ConsumeContext<DomainEvent.ShoppingStarted> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<SummaryEvent.CartProjectionRebuilt> context)
        => interactor.InteractAsync(context.Message, context.CancellationToken);
}