using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using PlaceOrderCommand = ECommerce.Contracts.Order.Commands.PlaceOrder;

namespace Application.UseCases.Commands;

public class PlaceOrderConsumer : IConsumer<PlaceOrderCommand>
{
    private readonly IOrderEventStoreService _eventStoreService;

    public PlaceOrderConsumer(IOrderEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<PlaceOrderCommand> context)
    {
        var order = new Order();
        order.Handle(context.Message);
        await _eventStoreService.AppendEventsToStreamAsync(order, context.Message, context.CancellationToken);
    }
}