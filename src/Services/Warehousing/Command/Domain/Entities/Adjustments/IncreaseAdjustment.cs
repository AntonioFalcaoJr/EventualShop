namespace Domain.Entities.Adjustments;

public class IncreaseAdjustment(string reason, int quantity) : IAdjustment
{
    public string Reason { get; } = reason;
    public int Quantity { get; } = quantity;
}