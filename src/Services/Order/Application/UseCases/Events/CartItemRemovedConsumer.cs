using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartItemRemovedEvent = Messages.ShoppingCarts.Events.CartItemRemoved;

namespace Application.UseCases.Events
{
    public class CartItemRemovedConsumer : IConsumer<CartItemRemovedEvent>
    {
        private readonly IOrderEventStoreService _eventStoreService;
        private readonly IOrderProjectionsService _projectionsService;

        public CartItemRemovedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartItemRemovedEvent> context)
        {
            var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
            var orderDetailsProjection = new OrderDetailsProjection();
            await _projectionsService.ProjectOrderDetailsAsync(orderDetailsProjection, context.CancellationToken);
        }
    }
}