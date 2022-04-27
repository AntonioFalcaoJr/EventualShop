using Application.EventStore;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.DebitCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using MassTransit;

namespace Application.UseCases.Events.Integrations;

public class PublishCartSubmittedWhenCheckedOutConsumer : IConsumer<DomainEvent.CartCheckedOut>
{
    private readonly IShoppingCartEventStoreService _eventStore;

    public PublishCartSubmittedWhenCheckedOutConsumer(IShoppingCartEventStoreService eventStore)
    {
        _eventStore = eventStore;
    }

    public async Task Consume(ConsumeContext<DomainEvent.CartCheckedOut> context)
    {
        var shoppingCart = await _eventStore.LoadAggregateAsync(context.Message.CartId, context.CancellationToken);

        var cartSubmittedEvent = new IntegrationEvent.CartSubmitted(
            CartId: shoppingCart.Id,
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
            ShoppingCartItems: shoppingCart.Items.Select(item => new Dto.ShoppingCartItem
            {
                Product = new()
                {
                    Description = "item.Product.Description",
                    Id = item.Product.Id,
                    Name = item.Product.Name,
                    Sku = item.Product.Sku,
                    PictureUrl = item.Product.PictureUrl,
                    UnitPrice = item.Product.UnitPrice
                },
                Quantity = item.Quantity
            }),
            Total: shoppingCart.Total,
            PaymentMethods: shoppingCart.PaymentMethods.Select<IPaymentMethod, Dto.IPaymentMethod>(method
                => method switch
                {
                    CreditCardPaymentMethod creditCard
                        => new Dto.CreditCard
                        {
                            Amount = creditCard.Amount,
                            Expiration = creditCard.Expiration,
                            Number = creditCard.Number,
                            HolderName = creditCard.HolderName,
                            SecurityNumber = creditCard.SecurityNumber
                        },
                    DebitCardPaymentMethod debitCard
                        => new Dto.DebitCard
                        {
                            Amount = debitCard.Amount,
                            Expiration = debitCard.Expiration,
                            Number = debitCard.Number,
                            HolderName = debitCard.HolderName,
                            SecurityNumber = debitCard.SecurityNumber
                        },
                    PayPalPaymentMethod payPal
                        => new Dto.PayPal
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