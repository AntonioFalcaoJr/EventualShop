using System.Linq;
using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Common;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;
using GetShoppingCartQuery = ECommerce.Contracts.ShoppingCart.Queries.GetShoppingCart;

namespace Application.UseCases.Queries;

public class GetShoppingCartConsumer : IConsumer<GetShoppingCartQuery>
{
    private readonly IShoppingCartProjectionsService _projectionsService;

    public GetShoppingCartConsumer(IShoppingCartProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetShoppingCartQuery> context)
    {
        var cartDetails = await _projectionsService.GetCartDetailsAsync(context.Message.UserId, context.CancellationToken);

        var response = new Responses.CartDetails
        {
            Id = cartDetails.Id,
            Total = cartDetails.Total,
            CartItems = cartDetails.CartItems.Select(projection
                => new Models.Item
                {
                    ProductId = projection.ProductId,
                    Quantity = projection.Quantity,
                    PictureUrl = projection.PictureUrl,
                    ProductName = projection.ProductName,
                    UnitPrice = projection.UnitPrice
                }),
            IsDeleted = cartDetails.IsDeleted,
            PaymentMethods = cartDetails.PaymentMethods.Select<IPaymentMethodProjection, Models.IPaymentMethod>(method
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
                }),
            UserId = cartDetails.UserId
        };

        await context.RespondAsync(response);
    }
}