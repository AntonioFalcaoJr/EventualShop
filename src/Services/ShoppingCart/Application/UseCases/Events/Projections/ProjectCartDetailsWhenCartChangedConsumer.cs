using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using BillingAddressChangedEvent = Messages.Services.ShoppingCarts.DomainEvents.BillingAddressChanged;
using CartCreatedEvent = Messages.Services.ShoppingCarts.DomainEvents.CartCreated;
using CartItemAddedEvent = Messages.Services.ShoppingCarts.DomainEvents.CartItemAdded;
using CreditCardAddedEvent = Messages.Services.ShoppingCarts.DomainEvents.CreditCardAdded;
using CartItemRemovedEvent = Messages.Services.ShoppingCarts.DomainEvents.CartItemRemoved;
using CartCheckedOutEvent = Messages.Services.ShoppingCarts.DomainEvents.CartCheckedOut;
using ShippingAddressAddedEvent = Messages.Services.ShoppingCarts.DomainEvents.ShippingAddressAdded;
using CartItemQuantityUpdatedEvent = Messages.Services.ShoppingCarts.DomainEvents.CartItemQuantityUpdated;
using CartItemQuantityIncreasedEvent = Messages.Services.ShoppingCarts.DomainEvents.CartItemQuantityIncreased;

namespace Application.UseCases.Events.Projections;

public class ProjectCartDetailsWhenCartChangedConsumer :
    IConsumer<BillingAddressChangedEvent>,
    IConsumer<CartCreatedEvent>,
    IConsumer<CartItemAddedEvent>,
    IConsumer<CreditCardAddedEvent>,
    IConsumer<CartItemRemovedEvent>,
    IConsumer<CartCheckedOutEvent>,
    IConsumer<ShippingAddressAddedEvent>,
    IConsumer<CartItemQuantityUpdatedEvent>,
    IConsumer<CartItemQuantityIncreasedEvent>
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
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartCreatedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemAddedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CreditCardAddedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemRemovedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartCheckedOutEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<ShippingAddressAddedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemQuantityUpdatedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    public Task Consume(ConsumeContext<CartItemQuantityIncreasedEvent> context)
        => Project(context.Message.CartId, context.CancellationToken);

    private async Task Project(Guid cartId, CancellationToken cancellationToken)
    {
        var cart = await _eventStoreService.LoadAggregateFromStreamAsync(cartId, cancellationToken);

        var accountDetails = new CartDetailsProjection
        {
            Id = cart.Id,
            IsDeleted = cart.IsDeleted,
            UserId = cart.UserId,
            Total = cart.Total,
            CartItems = cart.Items.Any()
                ? cart.Items.Select(item => new CartItemProjection
                    {
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
            // CreditCardProjection = cart.CreditCard is null
            //     ? default
            //     : new()
            //     {
            //         Expiration = cart.CreditCard.Expiration,
            //         Number = cart.CreditCard.Number,
            //         HolderName = cart.CreditCard.HolderName,
            //         SecurityNumber = cart.CreditCard.SecurityNumber
            //     },
            IsCheckedOut = cart.IsCheckedOut
        };

        await _projectionsService.ProjectAsync(accountDetails, cancellationToken);
    }
}