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
            //         BillingAddress = new Address
            //         {
            //             City = context.Message.BillingAddress.City,
            //             Country = context.Message.BillingAddress.Country,
            //             Number = context.Message.BillingAddress.Number,
            //             State = context.Message.BillingAddress.State,
            //             Street = context.Message.BillingAddress.Street,
            //             ZipCode = context.Message.BillingAddress.ZipCode
            //         },
            //         CreditCard = new CreditCard
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