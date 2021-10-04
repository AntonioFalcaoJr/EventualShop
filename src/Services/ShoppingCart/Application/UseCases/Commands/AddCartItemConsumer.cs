using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using AddCartItemCommand = Messages.ShoppingCarts.Commands.AddCartItem;

namespace Application.UseCases.Commands
{
    public class AddCartItemConsumer : IConsumer<AddCartItemCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public AddCartItemConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddCartItemCommand> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            cart.AddItem(
                cart.Id,
                context.Message.ProductId,
                context.Message.ProductName,
                context.Message.Quantity,
                context.Message.UnitPrice);

            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}