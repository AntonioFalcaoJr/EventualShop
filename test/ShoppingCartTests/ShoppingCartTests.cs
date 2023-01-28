using AutoFixture;
using Contracts.DataTransferObjects;
using Contracts.Services.ShoppingCart;
using Domain.Aggregates;
using Domain.Enumerations;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects;
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
    private readonly Guid _catalogId;
    private readonly Guid _customerId;
    private readonly Guid _inventoryId;
    private readonly Guid _itemId;
    private readonly Product _product;
    private readonly ushort _quantity;
    private readonly Money _unitPrice;

    public ShoppingCartTests()
    {
        _fixture = new();

        _cartId = _fixture.Create<Guid>();
        _itemId = _fixture.Create<Guid>();
        _customerId = _fixture.Create<Guid>();
        _catalogId = _fixture.Create<Guid>();
        _inventoryId = _fixture.Create<Guid>();
        _product = _fixture.Create<Product>();
        _quantity = (ushort)(_fixture.Create<ushort>() + 1);

        _unitPrice = _fixture.Build<Money>()
            .With(m => m.Amount, _fixture.Create<decimal>() + 1)
            .With(m => m.Currency, Currency.USD)
            .Create();
    }

    [Fact]
    public void CreateCartShouldRaiseCartCreated()
    {
        Dto.Money expectedTotal = Money.Zero(Currency.USD);

        Given<ShoppingCart>()
            .When<Command.CreateCart>(new(_customerId, Currency.USD))
            .Then<DomainEvent.CartCreated>(
                @event => @event.CustomerId.Should().Be(_customerId),
                @event => @event.Status.Should().Be(CartStatus.Active),
                @event => @event.Total.Should().Be(expectedTotal),
                @event => @event.Version.Should().Be(1));
    }

    [Fact]
    public void AddCartItemShouldRaiseCartItemAdded()
    {
        Dto.Money expectedUnitPrice = _unitPrice;
        Dto.Money expectedNewCartTotal = _unitPrice * _quantity;

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, _quantity, _unitPrice))
            .Then<DomainEvent.CartItemAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.Product.Should().BeEquivalentTo(_product),
                @event => @event.Quantity.Should().Be(_quantity),
                @event => @event.UnitPrice.Should().Be(expectedUnitPrice),
                @event => @event.InventoryId.Should().Be(_inventoryId),
                @event => @event.NewCartTotal.Should().Be(expectedNewCartTotal),
                @event => @event.Version.Should().Be(2));
    }

    [Fact]
    public void AddInvalidCartItemShouldThrowValidationException()
    {
        const ushort quantity = 0;

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, quantity, _unitPrice))
            .Throws<ValidationException>();
    }

    [Fact]
    public void ChangeCartItemQuantityForUpShouldRaiseCartItemIncreased()
    {
        Dto.Money cartItemAddedNewCartTotal = _unitPrice * _quantity;
        var changeCartItemQuantityNewQuantity = (ushort)(_quantity + 1);
        Dto.Money cartItemIncreasedNewCartTotal = cartItemAddedNewCartTotal + _unitPrice * changeCartItemQuantityNewQuantity;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _inventoryId, _product, _quantity, _unitPrice, cartItemAddedNewCartTotal, 2))
            .When<Command.ChangeCartItemQuantity>(new(_cartId, _itemId, changeCartItemQuantityNewQuantity))
            .Then<DomainEvent.CartItemIncreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(changeCartItemQuantityNewQuantity),
                @event => @event.NewCartTotal.Should().Be(cartItemIncreasedNewCartTotal),
                @event => @event.Version.Should().Be(3));
    }

    [Fact]
    public void ChangeCartItemQuantityForDownShouldRaiseCartItemDecreased()
    {
        var cartItemAddedNewCartTotal = _unitPrice * _quantity;

        var expectedNewQuantity = (ushort)(_quantity - 1);
        Dto.Money expectedNewCartTotal = cartItemAddedNewCartTotal - _unitPrice * expectedNewQuantity;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _inventoryId, _product, _quantity, _unitPrice, cartItemAddedNewCartTotal, 2))
            .When<Command.ChangeCartItemQuantity>(new(_cartId, _itemId, expectedNewQuantity))
            .Then<DomainEvent.CartItemDecreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(expectedNewQuantity),
                @event => @event.NewCartTotal.Should().Be(expectedNewCartTotal),
                @event => @event.Version.Should().Be(3));
    }

    [Fact]
    public void RemoveCartItemShouldRaiseCartItemRemoved()
    {
        var cartItemAddedNewCartTotal = _unitPrice * _quantity;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _inventoryId, _product, _quantity, _unitPrice, cartItemAddedNewCartTotal, 2))
            .When<Command.RemoveCartItem>(new(_cartId, _itemId))
            .Then<DomainEvent.CartItemRemoved>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.Version.Should().Be(3));
    }

    [Fact]
    public void AddCartItemWithSameProductShouldRaiseCartItemIncreased()
    {
        var cartItemAddedNewCartTotal = _unitPrice * _quantity;
        var addCartItemQuantity = (ushort)(_quantity + 1);

        var expectedNewQuantity = (ushort)(_quantity + addCartItemQuantity);
        Dto.Money expectedNewCartTotal = cartItemAddedNewCartTotal + _unitPrice * addCartItemQuantity;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _inventoryId, _product, _quantity, _unitPrice, cartItemAddedNewCartTotal, 2))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, _product, addCartItemQuantity, _unitPrice))
            .Then<DomainEvent.CartItemIncreased>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().Be(_itemId),
                @event => @event.NewQuantity.Should().Be(expectedNewQuantity),
                @event => @event.NewCartTotal.Should().Be(expectedNewCartTotal),
                @event => @event.Version.Should().Be(3));
    }

    [Fact]
    public void AddCartItemWithDifferentProductShouldRaiseCartItemAdded()
    {
        var product = _product with { Name = _fixture.Create<string>(), Brand = _fixture.Create<string>() };
        var cartItemAddedNewCartTotal = _unitPrice * _quantity;

        Dto.Money expectedUnitPrice = _unitPrice;
        Dto.Money expectedNewCartTotal = cartItemAddedNewCartTotal + _unitPrice * _quantity;

        Given<ShoppingCart>(
                new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1),
                new DomainEvent.CartItemAdded(_cartId, _itemId, _inventoryId, _product, _quantity, _unitPrice, cartItemAddedNewCartTotal, 2))
            .When<Command.AddCartItem>(new(_cartId, _catalogId, _inventoryId, product, _quantity, _unitPrice))
            .Then<DomainEvent.CartItemAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.ItemId.Should().NotBe(_itemId),
                @event => @event.Quantity.Should().Be(_quantity),
                @event => @event.UnitPrice.Should().Be(expectedUnitPrice),
                @event => @event.Product.Should().NotBeEquivalentTo(_product),
                @event => @event.NewCartTotal.Should().Be(expectedNewCartTotal),
                @event => @event.Version.Should().Be(3));
    }

    [Fact]
    public void AddBillingAddressShouldRaiseBillingAddressAdded()
    {
        var address = _fixture.Create<Address>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1))
            .When<Command.AddBillingAddress>(new(_cartId, address))
            .Then<DomainEvent.BillingAddressAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.Address.Should().BeEquivalentTo(address),
                @event => @event.Version.Should().Be(2));
    }

    [Fact]
    public void AddShippingAddressShouldRaiseShippingAddressAdded()
    {
        var address = _fixture.Create<Address>();

        Given<ShoppingCart>(new DomainEvent.CartCreated(_cartId, _customerId, Money.Zero(Currency.USD), CartStatus.Active, 1))
            .When<Command.AddShippingAddress>(new(_cartId, address))
            .Then<DomainEvent.ShippingAddressAdded>(
                @event => @event.CartId.Should().Be(_cartId),
                @event => @event.Address.Should().BeEquivalentTo(address),
                @event => @event.Version.Should().Be(2));
    }
}