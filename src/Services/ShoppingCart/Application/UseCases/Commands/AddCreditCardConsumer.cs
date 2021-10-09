using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddCreditCardCommand = Messages.ShoppingCarts.Commands.AddCreditCard;

namespace Application.UseCases.Commands
{
    public class AddCreditCardConsumer : IConsumer<AddCreditCardCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public AddCreditCardConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddCreditCardCommand> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            cart.AddCreditCard(
                context.Message.Expiration,
                context.Message.HolderName,
                context.Message.Number,
                context.Message.SecurityNumber);

            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}