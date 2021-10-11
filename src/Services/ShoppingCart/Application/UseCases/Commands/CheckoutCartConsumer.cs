using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using CheckoutCartCommand = Messages.ShoppingCarts.Commands.CheckoutCart;

namespace Application.UseCases.Commands
{
    public class CheckoutCartConsumer : IConsumer<CheckoutCartCommand>
    {
        private readonly IShoppingCartEventStoreService _eventStoreService;

        public CheckoutCartConsumer(IShoppingCartEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<CheckoutCartCommand> context)
        {
            var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

            // var cartCheckout = new Checkout
            // {
            //     Items = cart.Items,
            //     PaymentMethod = new PaymentMethod
            //     {
            //         BillingAddressProjection = new AddressProjection
            //         {
            //             City = context.Message.BillingAddressProjection.City,
            //             Country = context.Message.BillingAddressProjection.Country,
            //             Number = context.Message.BillingAddressProjection.Number,
            //             State = context.Message.BillingAddressProjection.State,
            //             Street = context.Message.BillingAddressProjection.Street,
            //             ZipCode = context.Message.BillingAddressProjection.ZipCode
            //         },
            //         CreditCardProjection = new CreditCardProjection
            //         {
            //             Expiration = context.Message.
            //         }
            //     }
            // };

            cart.CheckOut();
            await _eventStoreService.AppendEventsToStreamAsync(cart, context.CancellationToken);
        }
    }
}