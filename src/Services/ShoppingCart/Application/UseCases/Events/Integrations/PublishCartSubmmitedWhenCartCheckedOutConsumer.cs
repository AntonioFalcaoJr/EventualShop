using Application.EventStore;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.DebitCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.Events.Integrations;

public class PublishCartSubmittedWhenCheckedOutConsumer : IConsumer<DomainEvent.CartCheckedOut>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;

    public PublishCartSubmittedWhenCheckedOutConsumer(IShoppingCartEventStoreService eventStoreService)
    {
        _eventStoreService = eventStoreService;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.CartId, context.CancellationToken);

        var cartSubmittedEvent = new IntegrationEvent.CartSubmitted(
            ShoppingCartId: shoppingCart.Id,
            Customer: new()
            {
                Id = shoppingCart.Customer.Id,
                BillingAddress = new()
                {
                    City = shoppingCart.Customer.BillingAddress.City,
                    Country = shoppingCart.Customer.BillingAddress.Country,
                    Number = shoppingCart.Customer.BillingAddress.Number,
                    State = shoppingCart.Customer.BillingAddress.State,
                    Street = shoppingCart.Customer.BillingAddress.Street,
                    ZipCode = shoppingCart.Customer.BillingAddress.ZipCode
                },
                ShippingAddress = new()
                {
                    City = shoppingCart.Customer.ShippingAddress.City,
                    Country = shoppingCart.Customer.ShippingAddress.Country,
                    Number = shoppingCart.Customer.ShippingAddress.Number,
                    State = shoppingCart.Customer.ShippingAddress.State,
                    Street = shoppingCart.Customer.ShippingAddress.Street,
                    ZipCode = shoppingCart.Customer.ShippingAddress.ZipCode
                }
            },
            ShoppingCartItems: shoppingCart.Items.Select(item => new Models.ShoppingCartItem
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                PictureUrl = item.PictureUrl,
                ProductName = item.ProductName,
                UnitPrice = item.UnitPrice,
                Sku = item.Sku
            }),
            Total: shoppingCart.Total,
            PaymentMethods: shoppingCart.PaymentMethods.Select<IPaymentMethod, Models.IPaymentMethod>(method
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

        await context.Publish(cartSubmittedEvent, context.CancellationToken);
    }
}