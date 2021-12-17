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
using MassTransit;
using BillingAddressChangedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.BillingAddressChanged;
using CartCreatedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartCreated;
using CartItemAddedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemAdded;
using CreditCardAddedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CreditCardAdded;
using PayPalAddedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.PayPalAdded;
using CartItemRemovedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemRemoved;
using CartCheckedOutEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartCheckedOut;
using ShippingAddressAddedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.ShippingAddressAdded;
using CartItemQuantityIncreasedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemQuantityIncreased;
using CartItemQuantityDecreasedEvent = ECommerce.Contracts.ShoppingCart.DomainEvents.CartItemQuantityDecreased;

namespace Application.UseCases.Events.Projections;

public class ProjectCartDetailsWhenCartChangedConsumer :
    IConsumer<BillingAddressChangedEvent>,
    IConsumer<CartCreatedEvent>,
    IConsumer<CartItemAddedEvent>,
    IConsumer<CreditCardAddedEvent>,
    IConsumer<PayPalAddedEvent>,
    IConsumer<CartItemRemovedEvent>,
    IConsumer<CartCheckedOutEvent>,
    IConsumer<ShippingAddressAddedEvent>,
    IConsumer<CartItemQuantityIncreasedEvent>,
    IConsumer<CartItemQuantityDecreasedEvent>
{
    private readonly IShoppingCartEventStoreService _eventStoreService;
    private readonly IShoppingCartProjectionsService _projectionsService;

    public ProjectCartDetailsWhenCartChangedConsumer(
        IShoppingCartEventStoreService eventStoreService,
        IShoppingCartProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<BillingAddressChangedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartCreatedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemAddedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CreditCardAddedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<PayPalAddedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemRemovedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartCheckedOutEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<ShippingAddressAddedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemQuantityIncreasedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);
    
    public Task Consume(ConsumeContext<CartItemQuantityDecreasedEvent> context)
        => ProjectAsync(context.Message.CartId, context.CancellationToken);

    private async Task ProjectAsync(Guid cartId, CancellationToken cancellationToken)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(cartId, cancellationToken);

        var accountDetails = new CartDetailsProjection
        {
            Id = cart.Id,
            IsDeleted = cart.IsDeleted,
            UserId = cart.CustomerId,
            Total = cart.Total,
            CartItems = cart.Items.Any()
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
            BillingAddressProjection = cart.BillingAddress is null
                ? default
                : new()
                {
                    City = cart.BillingAddress.City,
                    Country = cart.BillingAddress.Country,
                    Number = cart.BillingAddress.Number,
                    State = cart.BillingAddress.State,
                    Street = cart.BillingAddress.Street,
                    ZipCode = cart.BillingAddress.ZipCode
                },
            ShippingAddressProjection = cart.ShippingAddress is null
                ? default
                : new()
                {
                    City = cart.ShippingAddress.City,
                    Country = cart.ShippingAddress.Country,
                    Number = cart.ShippingAddress.Number,
                    State = cart.ShippingAddress.State,
                    Street = cart.ShippingAddress.Street,
                    ZipCode = cart.ShippingAddress.ZipCode
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

        await _projectionsService.ProjectAsync(accountDetails, cancellationToken);
    }
}