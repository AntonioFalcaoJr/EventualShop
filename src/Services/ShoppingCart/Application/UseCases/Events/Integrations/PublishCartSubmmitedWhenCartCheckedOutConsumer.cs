using System.Linq;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages;
using CartCheckedOutEvent = Messages.Services.ShoppingCarts.DomainEvents.CartCheckedOut;
using CartSubmittedEvent = Messages.Services.ShoppingCarts.IntegrationEvents.CartSubmitted;

namespace Application.UseCases.Events.Integrations;

public class PublishCartSubmittedWhenCartCheckedOutConsumer : IConsumer<CartCheckedOutEvent>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public PublishCartSubmittedWhenCartCheckedOutConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<CartCheckedOutEvent> context)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

        var cartSubmittedEvent = new CartSubmittedEvent(
            CartId: cart.Id,
            CustomerId: cart.UserId,
            CartItems: cart.Items.Select(item => new Models.Item
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                PictureUrl = item.PictureUrl,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice
            }),
            BillingAddress: new Models.Address
            {
                City = cart.BillingAddress.City,
                Country = cart.BillingAddress.Country,
                Number = cart.BillingAddress.Number,
                State = cart.BillingAddress.State,
                Street = cart.BillingAddress.Street,
                ZipCode = cart.BillingAddress.ZipCode
            },
            CreditCard: new Models.CreditCard
            {
                Expiration = cart.CreditCard.Expiration,
                Number = cart.CreditCard.Number,
                HolderName = cart.CreditCard.HolderName,
                SecurityNumber = cart.CreditCard.SecurityNumber
            },
            ShippingAddress: new Models.Address
            {
                City = cart.ShippingAddress.City,
                Country = cart.ShippingAddress.Country,
                Number = cart.ShippingAddress.Number,
                State = cart.ShippingAddress.State,
                Street = cart.ShippingAddress.Street,
                ZipCode = cart.ShippingAddress.ZipCode
            });

        await context.Publish(cartSubmittedEvent);
    }
}