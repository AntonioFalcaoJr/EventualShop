using AutoFixture;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;
using Domain.Enumerations;
using Domain.ValueObjects.Products;
using FluentAssertions;
using FluentValidation;
using ShoppingCartTests.Abstractions;
using Xunit;

namespace ShoppingCartTests;

public class ShoppingCartTests : AggregateTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void CreateCartShouldRaiseCartCreated()
    {
        var cartId = _fixture.Create<Guid>();
        var customerId = _fixture.Create<Guid>();

        Given<ShoppingCart>()
            .When<Command.CreateCart>(new(cartId, customerId))
            .Then<DomainEvent.CartCreated>(
                @event => @event.Id.Should().Be(cartId),
                @event => @event.CustomerId.Should().Be(customerId),
                @event => @event.Status.Should().Be(CartStatus.Active));
    }

    [Fact]
    public void AddCartItemShouldRaiseCartItemAdded()
    {
        var cartId = _fixture.Create<Guid>();
        var itemId = _fixture.Create<Guid>();
        var customerId = _fixture.Create<Guid>();
        var catalogId = _fixture.Create<Guid>();
        var inventoryId = _fixture.Create<Guid>();
        var product = _fixture.Create<Product>();
        var quantity = _fixture.Create<ushort>();
        var unitPrice = _fixture.Create<decimal>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(cartId, customerId, CartStatus.Active))
            .When<Command.AddCartItem>(new(cartId, itemId, catalogId, inventoryId, product, quantity, unitPrice))
            .Then<DomainEvent.CartItemAdded>(
                @event => @event.Id.Should().Be(cartId),
                @event => @event.ItemId.Should().Be(itemId),
                // TODO - Remove ValueObject heritage from Product
                //@event => @event.Product.Should().Be(product),
                @event => @event.Quantity.Should().Be(quantity),
                @event => @event.UnitPrice.Should().Be(unitPrice),
                @event => @event.CatalogId.Should().Be(catalogId),
                @event => @event.InventoryId.Should().Be(inventoryId));
    }
    
    [Fact]
    public void AddInvalidCartItemShouldThrowValidationException()
    {
        var cartId = _fixture.Create<Guid>();
        var itemId = _fixture.Create<Guid>();
        var customerId = _fixture.Create<Guid>();
        var catalogId = _fixture.Create<Guid>();
        var inventoryId = _fixture.Create<Guid>();
        var product = _fixture.Create<Product>();
        var quantity = ushort.MinValue;
        var unitPrice = _fixture.Create<decimal>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(cartId, customerId, CartStatus.Active))
            .When<Command.AddCartItem>(new(cartId, itemId, catalogId, inventoryId, product, quantity, unitPrice))
            .Throws<ValidationException>(exception => exception.Message.Should().Be("Quantity must be greater than 0."));
    }

    [Fact]
    public void ChangeCartItemQuantityForUpShouldRaiseCartItemIncreased()
    {
        var cartId = _fixture.Create<Guid>();
        var itemId = _fixture.Create<Guid>();
        var customerId = _fixture.Create<Guid>();
        var catalogId = _fixture.Create<Guid>();
        var inventoryId = _fixture.Create<Guid>();
        var product = _fixture.Create<Product>();
        var quantity = _fixture.Create<ushort>();
        var newQuantity = (ushort)(quantity + 1);
        var unitPrice = _fixture.Create<decimal>();

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(cartId, customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(cartId, itemId, catalogId, inventoryId, product, quantity, unitPrice))
            .When<Command.ChangeCartItemQuantity>(new(cartId, itemId, newQuantity))
            .Then<DomainEvent.CartItemIncreased>(
                @event => @event.Id.Should().Be(cartId),
                @event => @event.ItemId.Should().Be(itemId),
                @event => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void RemoveCartItemShouldRaiseCartItemRemoved()
    {
        var cartId = _fixture.Create<Guid>();
        var itemId = _fixture.Create<Guid>();
        var customerId = _fixture.Create<Guid>();
        var catalogId = _fixture.Create<Guid>();
        var inventoryId = _fixture.Create<Guid>();
        var product = _fixture.Create<Product>();
        var quantity = _fixture.Create<ushort>();
        var newQuantity = (ushort)(quantity + 1);
        var unitPrice = _fixture.Create<decimal>();

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(cartId, customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(cartId, itemId, catalogId, inventoryId, product, quantity, unitPrice),
                new DomainEvent.CartItemIncreased(cartId, itemId, newQuantity, unitPrice))
            .When<Command.RemoveCartItem>(new(cartId, itemId))
            .Then<DomainEvent.CartItemRemoved>(
                @event => @event.Id.Should().Be(cartId),
                @event => @event.ItemId.Should().Be(itemId),
                @event => @event.Quantity.Should().Be(newQuantity),
                @event => @event.UnitPrice.Should().Be(unitPrice));
    }
}