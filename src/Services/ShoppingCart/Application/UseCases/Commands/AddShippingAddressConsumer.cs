using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddShippingAddressCommand = Messages.ShoppingCarts.Commands.AddShippingAddress;

namespace Application.UseCases.Commands
{
    public class AddShippingAddressConsumer : IConsumer<AddShippingAddressCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public AddShippingAddressConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddShippingAddressCommand> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            cart.AddShippingAddress(
                context.Message.City,
                context.Message.Country,
                context.Message.Number,
                context.Message.State,
                context.Message.Street,
                context.Message.ZipCode);

            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}