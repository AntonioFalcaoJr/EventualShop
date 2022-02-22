using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
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
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(cartId, cancellationToken);

        var cartProjection = new CartProjection
        {
            Id = cart.Id,
            IsDeleted = cart.IsDeleted,
            CustomerId = cart.CustomerId,
            Total = cart.Total,
            Items = cart.Items.Any()
                ? cart.Items.Select(item => new CartItemProjection
                    {
                        Id = item.Id,
                        Quantity = item.Quantity,
                        PictureUrl = item.PictureUrl,
                        ProductName = item.ProductName,
                        UnitPrice = item.UnitPrice,
                        ProductId = item.ProductId
                    }
                )
                : Enumerable.Empty<CartItemProjection>(),
            BillingAddressProjection = new()
            {
                City = cart.BillingAddress?.City,
                Country = cart.BillingAddress?.Country,
                Number = cart.BillingAddress?.Number,
                State = cart.BillingAddress?.State,
                Street = cart.BillingAddress?.Street,
                ZipCode = cart.BillingAddress?.ZipCode
            },
            ShippingAddressProjection = new()
            {
                City = cart.ShippingAddress?.City,
                Country = cart.ShippingAddress?.Country,
                Number = cart.ShippingAddress?.Number,
                State = cart.ShippingAddress?.State,
                Street = cart.ShippingAddress?.Street,
                ZipCode = cart.ShippingAddress?.ZipCode
            },
            PaymentMethods = cart.PaymentMethods.Select<IPaymentMethod, IPaymentMethodProjection>(method
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
            Status = cart.Status.ToString()
        };

        await _projectionsService.ProjectAsync(cartProjection, cancellationToken);
    }
}