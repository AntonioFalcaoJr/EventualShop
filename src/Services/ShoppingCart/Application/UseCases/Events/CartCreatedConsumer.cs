using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartCreatedEvent = Messages.ShoppingCarts.Events.CartCreated;

namespace Application.UseCases.Events
{
    public class CartCreatedConsumer : IConsumer<CartCreatedEvent>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;
        private readonly IShoppingCartProjectionsService _projectionsService;

        public CartCreatedConsumer(IShoppingCartEventStoreService eventStoreService, IShoppingCartProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartCreatedEvent> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            var accountDetails = new CartDetailsProjection
            {
                Id = cart.Id,
                UserId = cart.UserId
            };

            await _projectionsService.ProjectNewAsync(accountDetails, context.CancellationToken);
        }
    }
}