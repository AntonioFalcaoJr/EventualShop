using Application.EventStore;
using Contracts.Services.Order;
using Domain.Aggregates;
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
        Order order = new();
        order.Handle(context.Message);
        await _eventStore.AppendEventsAsync(order, context.CancellationToken);
    }
}