namespace Domain.Entities.Adjustments;

public class IncreaseAdjustment : IAdjustment
{
    public IncreaseAdjustment(string reason, int quantity)
    {
        Reason = reason;
        Quantity = quantity;
    }

    public string Reason { get; }
    public int Quantity { get; }
}