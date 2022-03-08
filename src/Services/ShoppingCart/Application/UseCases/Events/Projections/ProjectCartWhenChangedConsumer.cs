using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Entities.ShoppingCartItems;
using Domain.ValueObjects.PaymentMethods;
using Domain.ValueObjects.PaymentMethods.CreditCards;
using Domain.ValueObjects.PaymentMethods.DebitCards;
using Domain.ValueObjects.PaymentMethods.PayPal;
using ECommerce.Contracts.ShoppingCart;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectCartWhenChangedConsumer :
    IConsumer<DomainEvents.BillingAddressChanged>,
    IConsumer<DomainEvents.CartCreated>,
    IConsumer<DomainEvents.CartItemAdded>,
    IConsumer<DomainEvents.CreditCardAdded>,
    IConsumer<DomainEvents.PayPalAdded>,
    IConsumer<DomainEvents.CartItemRemoved>,
    IConsumer<DomainEvents.CartCheckedOut>,
    IConsumer<DomainEvents.ShippingAddressAdded>,
    IConsumer<DomainEvents.CartItemIncreased>,
    IConsumer<DomainEvents.CartItemDecreased>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;
    private readonly IShoppingCartProjectionsService _projectionsService;

    public ProjectCartWhenChangedConsumer(
        IShoppingCartEventStoreService eventStoreService,
        IShoppingCartProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.BillingAddressChanged> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartCreated> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemAdded> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CreditCardAdded> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.PayPalAdded> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemRemoved> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartCheckedOut> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.ShippingAddressAdded> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemIncreased> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.CartItemDecreased> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    private async Task ProjectAsync(Guid cartId, CancellationToken cancellationToken)
    {
        var shoppingCart = await _eventStoreService.LoadAggregateFromStreamAsync(cartId, cancellationToken);

        var cartProjection = new ShoppingCartProjection
        {
            Id = shoppingCart.Id,
            IsDeleted = shoppingCart.IsDeleted,
            Customer = new()
            {
                Id = shoppingCart.Customer.Id,
                BillingAddress = new()
                {
                    City = shoppingCart.Customer.BillingAddress?.City,
                    Country = shoppingCart.Customer.BillingAddress?.Country,
                    Number = shoppingCart.Customer.BillingAddress?.Number,
                    State = shoppingCart.Customer.BillingAddress?.State,
                    Street = shoppingCart.Customer.BillingAddress?.Street,
                    ZipCode = shoppingCart.Customer.BillingAddress?.ZipCode
                },
                ShippingAddress = new()
                {
                    City = shoppingCart.Customer.ShippingAddress?.City,
                    Country = shoppingCart.Customer.ShippingAddress?.Country,
                    Number = shoppingCart.Customer.ShippingAddress?.Number,
                    State = shoppingCart.Customer.ShippingAddress?.State,
                    Street = shoppingCart.Customer.ShippingAddress?.Street,
                    ZipCode = shoppingCart.Customer.ShippingAddress?.ZipCode
                }
            },
            Total = shoppingCart.Total,
            Items = shoppingCart.Items.Any()
                ? shoppingCart.Items
                    .Select<ShoppingCartItem, ShoppingCartItemProjection>(item
                        => new()
                        {
                            Id = item.Id,
                            Quantity = item.Quantity,
                            PictureUrl = item.PictureUrl,
                            ProductName = item.ProductName,
                            UnitPrice = item.UnitPrice,
                            ProductId = item.ProductId
                        }
                    )
                : default,
            PaymentMethods = shoppingCart.PaymentMethods
                .Select<IPaymentMethod, IPaymentMethodProjection>(method
                    => method switch
                    {
                        CreditCardPaymentMethod creditCard
                            => new CreditCardPaymentMethodProjection
                            {
                                Amount = creditCard.Amount,
                                Expiration = creditCard.Expiration,
                                Number = creditCard.Number,
                                HolderName = creditCard.HolderName,
                                SecurityNumber = creditCard.SecurityNumber
                            },
                        DebitCardPaymentMethod debitCard
                            => new DebitCardPaymentMethodProjection
                            {
                                Amount = debitCard.Amount,
                                Expiration = debitCard.Expiration,
                                Number = debitCard.Number,
                                HolderName = debitCard.HolderName,
                                SecurityNumber = debitCard.SecurityNumber
                            },
                        PayPalPaymentMethod payPal
                            => new PayPalPaymentMethodProjection
                            {
                                Amount = payPal.Amount,
                                Password = payPal.Password,
                                UserName = payPal.UserName
                            },
                        _ => default
                    }),
            Status = shoppingCart.Status.ToString()
        };

        await _projectionsService.ProjectAsync(cartProjection, cancellationToken);
    }
}