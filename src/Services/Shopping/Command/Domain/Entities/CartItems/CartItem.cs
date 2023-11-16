using Domain.Abstractions.Entities;
using Domain.Aggregates.Products;
using Domain.ValueObjects;

namespace Domain.Entities.CartItems;

public class CartItem : Entity<CartItemId>
{
    private Dictionary<Currency, Money> _totals = new();
    private readonly IDictionary<Currency, Price> _prices;

    public CartItem(CartItemId id, ProductId productId, ProductName productName, PictureUri pictureUri,
        Sku sku, IDictionary<Currency, Price> prices, Quantity quantity)
    {
        Id = id;
        ProductId = productId;
        ProductName = productName;
        PictureUri = pictureUri;
        Sku = sku;
        Quantity = quantity;
        _prices = prices;
        CalculateTotals();
    }

    public ProductId ProductId { get; }
    public ProductName ProductName { get; private set; }
    public PictureUri PictureUri { get; }
    public Sku Sku { get; }
    public Quantity Quantity { get; private set; }
    public IDictionary<Currency, Price> Prices => _prices.AsReadOnly();
    public IDictionary<Currency, Money> Totals => _totals.AsReadOnly();

    public void SetQuantity(Quantity quantity)
    {
        Quantity = quantity;
        CalculateTotals();
    }

    public void Delete()
        => IsDeleted = true;

    private void CalculateTotals()
        => _totals = Prices.ToDictionary(
            price => price.Key,
            price => price.Value * Quantity as Money);

    public static bool operator ==(CartItem left, CartItem right)
        => left.Id.Equals(right.Id) &&
           left.ProductId.Equals(right.ProductId);

    public static bool operator !=(CartItem left, CartItem right)
        => left.Id.Equals(right.Id) is false ||
           left.ProductId.Equals(right.ProductId) is false;

    public override bool Equals(object? obj)
    {
        if (obj is not CartItem item) return false;
        return Id.Equals(item.Id) && ProductId.Equals(item.ProductId);
    }

    public override int GetHashCode() => HashCode.Combine(Id, ProductId);
}