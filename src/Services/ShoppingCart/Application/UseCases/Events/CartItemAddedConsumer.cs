using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartItemAddedEvent = Messages.ShoppingCarts.Events.CartItemAdded;

namespace Application.UseCases.Events
{
    public class CartItemAddedConsumer : IConsumer<CartItemAddedEvent>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;
        private readonly IShoppingCartProjectionsService _projectionsService;

        public CartItemAddedConsumer(IShoppingCartEventStoreService eventStoreService, IShoppingCartProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartItemAddedEvent> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            var accountDetails = new CartDetailsProjection
                { };

            await _projectionsService.ProjectCartDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}