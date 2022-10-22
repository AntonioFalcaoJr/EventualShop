using AutoFixture;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.Products;
using FluentAssertions;
using FluentValidation;
using ShoppingCartTests.Abstractions;
using Xunit;

namespace ShoppingCartTests;

public class ShoppingCartTests : AggregateTests
{
    private readonly Fixture _fixture;

    private readonly Guid _cartId;
    private readonly Guid _itemId;
    private readonly Guid _customerId;
    private readonly Guid _catalogId;
    private readonly Guid _inventoryId;
    private readonly Product _product;
    private readonly ushort _quantity;
    private readonly decimal _unitPrice;

    public ShoppingCartTests()
    {
        _fixture = new();

        _cartId = _fixture.Create<Guid>();
        _itemId = _fixture.Create<Guid>();
        _customerId = _fixture.Create<Guid>();
        _catalogId = _fixture.Create<Guid>();
        _inventoryId = _fixture.Create<Guid>();
        _product = _fixture.Create<Product>();
        _quantity = _fixture.Create<ushort>();
        _unitPrice = _fixture.Create<decimal>();
    }

    [Fact]
    public void CreateCartShouldRaiseCartCreated()
        => Given<ShoppingCart>()
            .When<Command.CreateCart>(new(_cartId, _customerId))
            .Then<DomainEvent.CartCreated>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.CustomerId.Should().Be(_customerId),
                @event => @event.Status.Should().Be(CartStatus.Active));

    [Fact]
    public void AddCartItemShouldRaiseCartItemAdded()
        => Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .Then<DomainEvent.CartItemAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                // TODO - Remove ValueObject heritage from Product
                //@event => @event.Product.Should().Be(product),
                @event => @event.Quantity.Should().Be(_quantity),
                @event => @event.UnitPrice.Should().Be(_unitPrice),
                @event => @event.CatalogId.Should().Be(_catalogId),
                @event => @event.InventoryId.Should().Be(_inventoryId));

    [Fact]
    public void AddInvalidCartItemShouldThrowValidationException()
    {
        const ushort quantity = 0;

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, quantity, _unitPrice))
            .Throws<ValidationException>();
    }

    [Fact]
    public void ChangeCartItemQuantityForUpShouldRaiseCartItemIncreased()
    {
        var newQuantity = (ushort)(_quantity + 1);

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .When<Command.ChangeCartItemQuantity>(new(_cartId, _itemId, newQuantity))
            .Then<DomainEvent.CartItemIncreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void ChangeCartItemQuantityForDownShouldRaiseCartItemDecreased()
    {
        const ushort quantity = 10;
        const ushort newQuantity = quantity - 1;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _catalogId, _inventoryId, _product, quantity, _unitPrice))
            .When<Command.ChangeCartItemQuantity>(new(_cartId, _itemId, newQuantity))
            .Then<DomainEvent.CartItemDecreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void RemoveCartItemShouldRaiseCartItemRemoved()
    {
        var newQuantity = (ushort)(_quantity + 1);

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _catalogId, _inventoryId, _product, _quantity, _unitPrice),
                new DomainEvent.CartItemIncreased(_cartId, _itemId, newQuantity, _unitPrice))
            .When<Command.RemoveCartItem>(new(_cartId, _itemId))
            .Then<DomainEvent.CartItemRemoved>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.Quantity.Should().Be(newQuantity),
                @event => @event.UnitPrice.Should().Be(_unitPrice));
    }

    [Fact]
    public void AddCartItemWithSameProductShouldRaiseCartItemIncreased()
    {
        var newQuantity = (ushort)(_quantity * 2);

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .Then<DomainEvent.CartItemIncreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(newQuantity));
    }

    [Fact]
    public void AddCartItemWithDifferentProductShouldRaiseCartItemAdded()
    {
        var product = _fixture.Create<Product>();

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, product, _quantity, _unitPrice))
            .Then<DomainEvent.CartItemAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().NotBe(_itemId),
                @event => @event.Quantity.Should().Be(_quantity));
    }

    [Fact]
    public void AddBillingAddressShouldRaiseBillingAddressAdded()
    {
        var address = _fixture.Create<Address>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active))
            .When<Command.AddBillingAddress>(new(_cartId, address))
            .Then<DomainEvent.BillingAddressAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.Address.Should().BeEquivalentTo(address));
    }

    [Fact]
    public void AddShippingAddressShouldRaiseShippingAddressAdded()
    {
        var address = _fixture.Create<Address>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, CartStatus.Active))
            .When<Command.AddShippingAddress>(new(_cartId, address))
            .Then<DomainEvent.ShippingAddressAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.Address.Should().BeEquivalentTo(address));
    }
}