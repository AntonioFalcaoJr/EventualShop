using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Orders;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectOrderDetailsWhenOrderChangedConsumer : IConsumer<DomainEvents.OrderPlaced>
{
    private readonly IOrderEventStoreService _eventStoreService;
    private readonly IOrderProjectionsService _projectionsService;

    public ProjectOrderDetailsWhenOrderChangedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<DomainEvents.OrderPlaced> context)
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