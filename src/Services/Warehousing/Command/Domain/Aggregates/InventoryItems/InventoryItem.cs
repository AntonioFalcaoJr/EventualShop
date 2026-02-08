using Domain.Abstractions.Entities;
using Domain.Entities;
using Domain.Entities.Adjustments;
using Newtonsoft.Json;

namespace Domain.Aggregates.InventoryItems;

public class InventoryItem : Entity<InventoryItemValidator>
{
    [JsonProperty]
    private readonly List<Reserve> _reserves = [];

    [JsonProperty]
    private readonly List<IAdjustment> _adjustments = [];

    public InventoryItem(Guid id, decimal cost, ProductId productId, int quantity, string sku)
    {
        Id = id;
        Cost = cost;
        Product = product;
        Quantity = quantity;
        Sku = sku;
    }

    public int QuantityAvailable
        => Quantity - QuantityReserved;

    public int QuantityReserved
        => _reserves.Sum(reserve => reserve.Quantity);

    public int TotalAdjustments
        => _adjustments.Sum(adjustment
            => adjustment switch
            {
                IncreaseAdjustment => adjustment.Quantity,
                DecreaseAdjustment => adjustment.Quantity * -1,
                _ => default
            });

    public IEnumerable<Reserve> Reserves
        => _reserves.AsReadOnly();

    public IEnumerable<IAdjustment> Adjustments
        => _adjustments.AsReadOnly();

    public ProductId Product { get; }
    public int Quantity { get; private set; }
    public int MaxPurchaseQuantity { get; }
    public decimal Cost { get; }
    public string Sku { get; }

    public void Increase(int quantity)
        => Quantity += quantity;

    public void Decrease(int quantity)
        => Quantity -= quantity;

    public void Adjust(IAdjustment adjustment)
    {
        _adjustments.Add(adjustment);

        // Quantity = adjustment switch
        // {
        //     IncreaseAdjustment => Quantity += adjustment.Quantity,
        //     DecreaseAdjustment => Quantity -= adjustment.Quantity,
        //     _ => Quantity
        // };
    }

    public void Reserve(int quantity, Guid cartId, DateTimeOffset expiration)
        => _reserves.Add(new()
        {
            Quantity = quantity,
            CartId = cartId,
            Expiration = expiration
        });
}