using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Orders;
using MassTransit;

namespace Application.UseCases.CommandHandlers;

public class PlaceOrderConsumer : IConsumer<Commands.PlaceOrder>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public PlaceOrderConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<Commands.PlaceOrder> context)
    {
        var order = new Order();
        order.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
    }
}