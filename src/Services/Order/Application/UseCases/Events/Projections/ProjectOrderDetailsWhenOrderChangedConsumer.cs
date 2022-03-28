using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using OrderPlacedEvent = ECommerce.Contracts.Order.DomainEvents.OrderPlaced;

namespace Application.UseCases.Events.Projections;

public class ProjectOrderDetailsWhenOrderChangedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IOrderEventStoreService _eventStoreService;
    private readonly IOrderProjectionsService _projectionsService;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<OrderPlacedEvent> context)
    {
        var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.OrderId, context.CancellationToken);
            
        var orderDetailsProjection = new OrderDetailsProjection
        {
            Id = order.Id,
            IsDeleted = order.IsDeleted
        };
            
        await _projectionsService.ProjectAsync(orderDetailsProjection, context.CancellationToken);
    }
}