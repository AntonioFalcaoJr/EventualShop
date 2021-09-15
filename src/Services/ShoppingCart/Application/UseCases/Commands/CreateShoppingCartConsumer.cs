using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.ShoppingCarts;
using MassTransit;
using Messages.ShoppingCarts.Commands;

namespace Application.UseCases.Commands
{
    public class CreateShoppingCartConsumer : IConsumer<CreateShoppingCart>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public CreateShoppingCartConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<CreateShoppingCart> context)
        {
            var shoppingCart = new ShoppingCart();
            shoppingCart.Create(context.Message.CustomerId);
            await _eventStoreService.AppendEventsToStreamAsync(shoppingCart, context.CancellationToken);
        }
    }
}