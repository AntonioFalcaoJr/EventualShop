using System.Linq;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using CartItemRemovedEvent = Messages.ShoppingCarts.Events.CartItemRemoved;

namespace Application.UseCases.Events
{
    public class CartItemRemovedConsumer : IConsumer<CartItemRemovedEvent>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;
        private readonly IShoppingCartProjectionsService _projectionsService;

        public CartItemRemovedConsumer(IShoppingCartEventStoreService eventStoreService, IShoppingCartProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<CartItemRemovedEvent> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            var accountDetails = new CartDetailsProjection
            {
                Id = cart.Id,
                IsDeleted = cart.IsDeleted,
                UserId = cart.UserId,
                Total = cart.Total,
                CartItems = cart.Items
                    .Select(item => new CartItemProjection
                    {
                        Quantity = item.Quantity,
                        PictureUrl = item.PictureUrl,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        CatalogItemId = item.CatalogItemId
                    })
            };

            await _projectionsService.UpdateCartDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}