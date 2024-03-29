using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCartDetailsWhenChangedConsumer :
    IConsumer<DomainEvent.CartCreated>,
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartCheckedOut>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartItemDecreased>,
    IConsumer<DomainEvent.CartDiscarded>,
    IConsumer<SummaryEvent.CartProjectionRebuilt>
{
    private readonly IProjectCartDetailsWhenCartChangedInteractor _interactor;

    public ProjectCartDetailsWhenChangedConsumer(IProjectCartDetailsWhenCartChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CartCreated> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<SummaryEvent.CartProjectionRebuilt> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}