using Application.UseCases.Events;
using Contracts.Services.ShoppingCart;
using MassTransit;

namespace Infrastructure.EventBus.Consumers.Events;

public class ProjectCartItemListItemWhenCartChangedConsumer :
    IConsumer<DomainEvent.CartItemAdded>,
    IConsumer<DomainEvent.CartItemRemoved>,
    IConsumer<DomainEvent.CartItemIncreased>,
    IConsumer<DomainEvent.CartDiscarded>,
    IConsumer<DomainEvent.CartItemDecreased>
{
    private readonly IProjectCartItemListItemWhenCartChangedInteractor _interactor;

    public ProjectCartItemListItemWhenCartChangedConsumer(IProjectCartItemListItemWhenCartChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.CartItemAdded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemRemoved> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemIncreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartDiscarded> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.CartItemDecreased> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}