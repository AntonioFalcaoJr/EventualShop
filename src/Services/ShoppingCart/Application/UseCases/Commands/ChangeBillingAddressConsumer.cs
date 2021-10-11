using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using ChangeBillingAddressCommand = Messages.ShoppingCarts.Commands.ChangeBillingAddress;

namespace Application.UseCases.Commands
{
    public class ChangeBillingAddressConsumer : IConsumer<ChangeBillingAddressCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public ChangeBillingAddressConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<ChangeBillingAddressCommand> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            cart.ChangeBillingAddress(
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