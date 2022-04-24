using Application.EventStore;
using Domain.Aggregates;
using ECommerce.Contracts.Orders;
using MassTransit;

namespace Application.UseCases.Commands;

public class PlaceOrderConsumer : IConsumer<Command.PlaceOrder>
{
    private readonly IOrderEventStoreService _eventStore;

    public PlaceOrderConsumer(IOrderEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<Command.PlaceOrder> context)
    {
        var order = new Order();
        order.Handle(context.Message);
        await _eventStore.AppendEventsAsync(order, context.CancellationToken);
    }
}