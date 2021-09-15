using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.CartItems;
using MassTransit;
using Messages.ShoppingCarts.Commands;

namespace Application.UseCases.Commands
{
    public class AddShoppingCartItemConsumer : IConsumer<AddShoppingCartItem>
    {
        private readonly IShoppingCartEventStoreService _storeService;

        public AddShoppingCartItemConsumer(IShoppingCartEventStoreService storeService)
        {
            _storeService = storeService;
        }

        public async Task Consume(ConsumeContext<AddShoppingCartItem> context)
        {
            var shoppingCartItem = new ShoppingCartItem(
                context.Message.CatalogItemId,
                context.Message.CatalogItemName,
                context.Message.UnitPrice,
                context.Message.Quantity);

            var shoppingCart = await _storeService.LoadAggregateFromStreamAsync(context.Message.ShoppingCartId, context.CancellationToken);

            shoppingCart.AddItem(shoppingCartItem);

            if (shoppingCart.IsValid is false)
            {
                //_notificationContext.Add(shoppingCart.Errors);
                return;
            }

            await _storeService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
        }
    }
}