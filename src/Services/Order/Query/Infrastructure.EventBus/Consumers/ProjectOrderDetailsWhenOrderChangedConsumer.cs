using Application.UseCases.Events;
using Contracts.Services.Order;
using MassTransit;

namespace Infrastructure.EventBus.Consumers;

public class ProjectOrderDetailsWhenOrderChangedConsumer :
    IConsumer<DomainEvent.OrderPlaced>,
    IConsumer<DomainEvent.OrderConfirmed>
{
    private readonly IProjectOrderDetailsWhenOrderChangedInteractor _interactor;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IProjectOrderDetailsWhenOrderChangedInteractor interactor)
    {
        _interactor = interactor;
    }

    public Task Consume(ConsumeContext<DomainEvent.OrderConfirmed> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.OrderPlaced> context)
        => _interactor.InteractAsync(context.Message, context.CancellationToken);
}