using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartItemAddedEvent = Messages.ShoppingCarts.Events.CartItemAdded;

namespace Application.UseCases.Events
{
    public class CartItemAddedConsumer : IConsumer<CartItemAddedEvent>
    {
        private readonly IOrderEventStoreService _eventStoreService;
        private readonly IOrderProjectionsService _projectionsService;

        public CartItemAddedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartItemAddedEvent> context)
        {
            var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
            var orderDetailsProjection = new OrderDetailsProjection();
            await _projectionsService.ProjectOrderDetailsAsync(orderDetailsProjection, context.CancellationToken);
        }
    }
}