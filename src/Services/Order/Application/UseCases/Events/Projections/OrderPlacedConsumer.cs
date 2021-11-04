using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using OrderPlacedEvent = Messages.Services.Orders.Events.OrderPlaced;

namespace Application.UseCases.Events.Projections;

public class OrderPlacedConsumer : IConsumer<OrderPlacedEvent>
{
    private readonly IOrderEventStoreService _eventStoreService;
    private readonly IOrderProjectionsService _projectionsService;

    public OrderPlacedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
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
            
        await _projectionsService.ProjectOrderDetailsAsync(orderDetailsProjection, context.CancellationToken);
    }
}