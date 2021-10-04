using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.ShoppingCarts;
using MassTransit;
using CreateShoppingCartCommand = Messages.ShoppingCarts.Commands.CreateShoppingCart;

namespace Application.UseCases.Commands
{
    public class CreateShoppingCartConsumer : IConsumer<CreateShoppingCartCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public CreateShoppingCartConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<CreateShoppingCartCommand> context)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.Create(context.Message.CustomerId);
            await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
        }
    }
}