using System.Linq;
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
                    }),
                BillingAddressProjection = cart.BillingAddress is null
                    ? default
                    : new AddressProjection
                    {
                        City = cart.BillingAddress.City,
                        Country = cart.BillingAddress.Country,
                        Number = cart.BillingAddress.Number,
                        State = cart.BillingAddress.State,
                        Street = cart.BillingAddress.Street,
                        ZipCode = cart.BillingAddress.ZipCode
                    },
                ShippingAddressProjection = cart.ShippingAddress is null
                    ? default
                    : new AddressProjection
                    {
                        City = cart.ShippingAddress.City,
                        Country = cart.ShippingAddress.Country,
                        Number = cart.ShippingAddress.Number,
                        State = cart.ShippingAddress.State,
                        Street = cart.ShippingAddress.Street,
                        ZipCode = cart.ShippingAddress.ZipCode
                    },
                CreditCardProjection = cart.CreditCard is null
                    ? default
                    : new CreditCardProjection
                    {
                        Expiration = cart.CreditCard.Expiration,
                        Number = cart.CreditCard.Number,
                        HolderName = cart.CreditCard.HolderName,
                        SecurityNumber = cart.CreditCard.SecurityNumber
                    },
                IsCheckedOut = cart.IsCheckedOut
            };

            await _projectionsService.UpdateCartDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}