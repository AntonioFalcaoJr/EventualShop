using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCarts;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetShoppingCartDetailsConsumer :
    IConsumer<Queries.GetShoppingCart>,
    IConsumer<Queries.GetCustomerShoppingCart>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartDetailsConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<Queries.GetShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionsService.GetShoppingCartAsync(context.Message.CartId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    public async Task Consume(ConsumeContext<Queries.GetCustomerShoppingCart> context)
    {
        var shoppingCartProjection = await _projectionsService.GetShoppingCartByCustomerIdAsync(context.Message.CustomerId, context.CancellationToken);
        await RespondAsync(shoppingCartProjection, context);
    }

    private static Task RespondAsync(ShoppingCartProjection projection, ConsumeContext context)
        => projection is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(MapToCartDetailsResponse(projection));

    private static Responses.ShoppingCart MapToCartDetailsResponse(ShoppingCartProjection shoppingCartProjection)
        => new()
        {
            Id = shoppingCartProjection.Id,
            Total = shoppingCartProjection.Total,
            Customer = new()
            {
                Id = shoppingCartProjection.Customer.Id,
                BillingAddress = new()
                {
                    City = shoppingCartProjection.Customer.BillingAddress.City,
                    Country = shoppingCartProjection.Customer.BillingAddress.Country,
                    Number = shoppingCartProjection.Customer.BillingAddress.Number,
                    State = shoppingCartProjection.Customer.BillingAddress.State,
                    Street = shoppingCartProjection.Customer.BillingAddress.Street,
                    ZipCode = shoppingCartProjection.Customer.BillingAddress.ZipCode
                },
                ShippingAddress = new()
                {
                    City = shoppingCartProjection.Customer.ShippingAddress.City,
                    Country = shoppingCartProjection.Customer.ShippingAddress.Country,
                    Number = shoppingCartProjection.Customer.ShippingAddress.Number,
                    State = shoppingCartProjection.Customer.ShippingAddress.State,
                    Street = shoppingCartProjection.Customer.ShippingAddress.Street,
                    ZipCode = shoppingCartProjection.Customer.ShippingAddress.ZipCode
                }
            },
            Items = shoppingCartProjection.Items?.Select(projection
                => new Models.ShoppingCartItem
                {
                    Id = projection.Id,
                    ProductId = projection.ProductId,
                    Quantity = projection.Quantity,
                    PictureUrl = projection.PictureUrl,
                    ProductName = projection.ProductName,
                    UnitPrice = projection.UnitPrice
                }),
            IsDeleted = shoppingCartProjection.IsDeleted,
            PaymentMethods = shoppingCartProjection.PaymentMethods
                .Select<IPaymentMethodProjection, Models.IPaymentMethod>(method
                    => method switch
                    {
                        CreditCardPaymentMethodProjection creditCard
                            => new Models.CreditCard
                            {
                                Amount = creditCard.Amount,
                                Expiration = creditCard.Expiration,
                                Number = creditCard.Number,
                                HolderName = creditCard.HolderName,
                                SecurityNumber = creditCard.SecurityNumber
                            },
                        DebitCardPaymentMethodProjection debitCard
                            => new Models.DebitCard
                            {
                                Amount = debitCard.Amount,
                                Expiration = debitCard.Expiration,
                                Number = debitCard.Number,
                                HolderName = debitCard.HolderName,
                                SecurityNumber = debitCard.SecurityNumber
                            },
                        PayPalPaymentMethodProjection payPal
                            => new Models.PayPal
                            {
                                Amount = payPal.Amount,
                                Password = payPal.Password,
                                UserName = payPal.UserName
                            },
                        _ => default
                    })
        };
}