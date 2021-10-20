using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using PlaceOrderCommand = Messages.Orders.Commands.PlaceOrder;

namespace Application.UseCases.Commands
{
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

            order.Place(
                context.Message.CustomerId,
                context.Message.Items,
                context.Message.BillingAddress,
                context.Message.CreditCard,
                context.Message.ShippingAddress
            );

            await _eventStoreService.AppendEventsToStreamAsync(order, context.CancellationToken);
        }
    }
}