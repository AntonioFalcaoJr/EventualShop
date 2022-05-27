using Domain.Abstractions.Entities;
using Domain.Entities.Adjustments;
using Domain.ValueObjects.Products;

namespace Domain.Entities.InventoryItems;

public class InventoryItem : Entity<Guid, InventoryItemValidator>
{
    private readonly List<Reserve> _reserves = new();
    private readonly List<IAdjustment> _adjustments = new();

    public InventoryItem(Guid id, decimal cost, Product product, int quantity)
    {
        Id = id;
        Cost = cost;
        Product = product;
        Quantity = quantity;
    }

    public int QuantityAvailable
        => Quantity - QuantityReserved;

    public int QuantityReserved
        => _reserves.Sum(reserve => reserve.Quantity);

    public int Adjustment
        => _adjustments.Sum(adjustment
            => adjustment switch
            {
                IncreaseAdjustment => adjustment.Quantity,
                DecreaseAdjustment => adjustment.Quantity * -1,
                _ => default
            });

    public IEnumerable<Reserve> Reserves
        => _reserves;

    public IEnumerable<IAdjustment> Adjustments
        => _adjustments;

    public Product Product { get; }
    public int Quantity { get; private set; }
    public decimal Cost { get; private set; }

    public void Increase(int quantity)
        => Quantity += quantity;

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

    public void Reserve(int quantity, Guid cartId)
        => _reserves.Add(new()
        {
            Quantity = quantity,
            CartId = cartId
        });
}