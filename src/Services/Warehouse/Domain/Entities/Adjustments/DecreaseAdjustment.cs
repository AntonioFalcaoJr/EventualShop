namespace Domain.Entities.Adjustments;

public class DecreaseAdjustment : IAdjustment
{
    public DecreaseAdjustment(string reason, int quantity)
    {
        Reason = reason;
        Quantity = quantity;
    }

    public string Reason { get; }
    public int Quantity { get; }
}