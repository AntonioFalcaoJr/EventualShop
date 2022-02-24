using System.Linq;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.DebitCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Contracts.Common;
using MassTransit;
using CartCheckedOutEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartCheckedOut;
using CartSubmittedEvent = ECommerce.Contracts.ShoppingCart.IntegrationEvents.CartSubmitted;

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
            CustomerId: cart.CustomerId,
            CartItems: cart.Items.Select(item => new Models.ShoppingCartItem
            {
                Id = item.Id,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                PictureUrl = item.PictureUrl,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice
            }),
            BillingAddress: new()
            {
                City = cart.BillingAddress.City,
                Country = cart.BillingAddress.Country,
                Number = cart.BillingAddress.Number,
                State = cart.BillingAddress.State,
                Street = cart.BillingAddress.Street,
                ZipCode = cart.BillingAddress.ZipCode
            },
            ShippingAddress: new()
            {
                City = cart.ShippingAddress.City,
                Country = cart.ShippingAddress.Country,
                Number = cart.ShippingAddress.Number,
                State = cart.ShippingAddress.State,
                Street = cart.ShippingAddress.Street,
                ZipCode = cart.ShippingAddress.ZipCode
            },
            Total: cart.Total,
            PaymentMethods: cart.PaymentMethods.Select<IPaymentMethod, Models.IPaymentMethod>(method
                => method switch
                {
                    CreditCardPaymentMethod creditCard
                        => new Models.CreditCard
                        {
                            Amount = creditCard.Amount,
                            Expiration = creditCard.Expiration,
                            Number = creditCard.Number,
                            HolderName = creditCard.HolderName,
                            SecurityNumber = creditCard.SecurityNumber
                        },
                    DebitCardPaymentMethod debitCard
                        => new Models.DebitCard
                        {
                            Amount = debitCard.Amount,
                            Expiration = debitCard.Expiration,
                            Number = debitCard.Number,
                            HolderName = debitCard.HolderName,
                            SecurityNumber = debitCard.SecurityNumber
                        },
                    PayPalPaymentMethod payPal
                        => new Models.PayPal
                        {
                            Amount = payPal.Amount,
                            Password = payPal.Password,
                            UserName = payPal.UserName
                        },
                    _ => default
                }));

        await context.Publish(cartSubmittedEvent);
    }
}