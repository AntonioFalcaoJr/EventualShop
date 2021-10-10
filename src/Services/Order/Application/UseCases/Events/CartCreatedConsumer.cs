using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartCreatedEvent = Messages.ShoppingCarts.Events.CartCreated;

namespace Application.UseCases.Events
{
    public class CartCreatedConsumer : IConsumer<CartCreatedEvent>
    {
        private readonly IOrderEventStoreService _eventStoreService;
        private readonly IOrderProjectionsService _projectionsService;

        public CartCreatedConsumer(IOrderEventStoreService eventStoreService, IOrderProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartCreatedEvent> context)
        {
            var order = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);
            var orderDetailsProjection = new OrderDetailsProjection();
            await _projectionsService.ProjectOrderDetailsAsync(orderDetailsProjection, context.CancellationToken);
        }
    }
}