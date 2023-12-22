using Contracts.Abstractions.Messages;
using Contracts.Boundaries.Shopping.ShoppingCart;
using Domain.Abstractions.Aggregates;
using Domain.Aggregates.Products;
using Domain.Entities.CartItems;
using Domain.Enumerations;
using Domain.Extensions;
using Domain.ValueObjects;
using Newtonsoft.Json;
using static Domain.Exceptions;
using Version = Domain.ValueObjects.Version;

namespace Domain.Aggregates.ShoppingCarts;

public class ShoppingCart : AggregateRoot<CartId>
{
    [JsonProperty]
    private readonly Dictionary<ProductId, CartItem> _items = new();

    [JsonProperty]
    private IDictionary<Currency, Money> _totals =
        Currency.All.ToDictionary(Currency (currency) => currency, Money.Zero);

    public CustomerId CustomerId { get; private set; } = CustomerId.Undefined;
    public CartStatus Status { get; private set; } = CartStatus.Empty;
    public IDictionary<Currency, Money> Totals => _totals.AsReadOnly();
    public IEnumerable<CartItem> Items => _items.Values;

    public static ShoppingCart StartShopping(CustomerId customerId)
    {
        ShoppingCart cart = new();
        DomainEvent.ShoppingStarted @event = new(cart.Id, customerId, CartStatus.Open, Version.Initial);
        cart.RaiseEvent(@event);
        return cart;
    }

    public void AddItem(CartItem newItem)
    {
        CartNotOpen.ThrowIf(Status is not CartStatusOpen);

        _items.TryGetValue(newItem.ProductId, out var item);

        RaiseEvent(item is { IsDeleted: false } ? ItemIncreased() : ItemAdded());

        DomainEvent.CartItemIncreased ItemIncreased()
            => new(Id, item.Id, item.ProductId, item.Quantity + newItem.Quantity,
                item.Prices.AsString(), Totals.Project(newItem), Version.Next);

        DomainEvent.CartItemAdded ItemAdded()
            => new(Id, newItem.Id, newItem.ProductId, newItem.ProductName, newItem.PictureUri, newItem.Sku,
                newItem.Quantity, newItem.Prices.AsString(), Totals.Project(newItem), Version.Next);
    }

    public void ChangeItemQuantity(ProductId productId, Quantity newQuantity)
    {
        CartNotOpen.ThrowIf(Status is not CartStatusOpen);

        _items.TryGetValue(productId, out var item);

        CartItemNotFound.ThrowIf(item is not { IsDeleted: false });

        RaiseEvent(newQuantity switch
        {
            _ when newQuantity == Quantity.Zero => ItemRemoved(),
            _ when newQuantity < item!.Quantity => ItemDecreased(),
            _ when newQuantity > item.Quantity => ItemIncreased(),
            _ => InvalidQuantity.Throw()
        });

        DomainEvent.CartItemIncreased ItemIncreased() =>
            new(Id, item.Id, item.ProductId, newQuantity, item.Prices.AsString(),
                Totals.Project(item.Prices, newQuantity), Version.Next);

        DomainEvent.CartItemDecreased ItemDecreased() =>
            new(Id, item.Id, item.ProductId, newQuantity, item.Prices.AsString(),
                Totals.Project(item.Prices, newQuantity), Version.Next);

        DomainEvent.CartItemRemoved ItemRemoved() =>
            new(Id, item!.Id, item.ProductId, item.Prices.AsString(),
                Totals.Project(item.Prices, newQuantity), Version.Next);
    }

    public void RemoveItem(ProductId productId)
    {
        _items.TryGetValue(productId, out var item);

        CartItemNotFound.ThrowIf(item is not { IsDeleted: false });
        
        RaiseEvent(new DomainEvent.CartItemRemoved(Id, item!.Id, item.ProductId, 
            item.Prices.AsString(), Totals.Project(item.Prices, Quantity.Zero), Version.Next));
    }

    public void CheckOut()
    {
        CartNotOpen.ThrowIf(Status is not CartStatusOpen);
        CartIsEmpty.ThrowIf(_items is { Count: 0 });

        RaiseEvent(new DomainEvent.CartCheckedOut(Id, CartStatus.CheckedOut, Version.Next));
    }

    public void Discard()
    {
        if (Status is CartStatusAbandoned) return;
        RaiseEvent(new DomainEvent.CartDiscarded(Id, CartStatus.Abandoned, Version.Next));
    }

    protected override void ApplyEvent(IDomainEvent @event) => When(@event as dynamic);

    private void When(DomainEvent.ShoppingStarted @event)
    {
        Id = (CartId)@event.CartId;
        CustomerId = (CustomerId)@event.CustomerId;
        Status = (CartStatus)@event.Status;
    }

    private void When(DomainEvent.CartCheckedOut @event)
        => Status = (CartStatus)@event.Status;

    private void When(DomainEvent.CartDiscarded @event)
    {
        Status = (CartStatus)@event.Status;
        IsDeleted = true;
    }

    private void When(DomainEvent.CartItemAdded @event)
    {
        var itemId = (CartItemId)@event.ItemId;
        var productId = (ProductId)@event.ProductId;
        var productName = (ProductName)@event.ProductName;
        var pictureUri = (PictureUri)@event.PictureUri;
        var sku = (Sku)@event.Sku;
        var quantity = (Quantity)@event.Quantity;

        var prices = @event.Prices.ToPriceDictionary();

        _items[productId] = new(itemId, productId, productName, pictureUri, sku, prices, quantity);

        _totals = @event.Totals.ToMoneyDictionary();
    }

    private void When(DomainEvent.CartItemIncreased @event)
    {
        var productId = (ProductId)@event.ProductId;
        var newQuantity = (Quantity)@event.NewQuantity;

        _items[productId].SetQuantity(newQuantity);
        _totals = @event.Totals.ToMoneyDictionary();
    }

    private void When(DomainEvent.CartItemDecreased @event)
    {
        var productId = (ProductId)@event.ProductId;
        var newQuantity = (Quantity)@event.NewQuantity;

        _items[productId].SetQuantity(newQuantity);
        _totals = @event.Totals.ToMoneyDictionary();
    }

    private void When(DomainEvent.CartItemRemoved @event)
    {
        var productId = (ProductId)@event.ProductId;

        _items[productId].Delete();
        _totals = @event.Totals.ToMoneyDictionary();
    }
}