namespace Domain.Entities.Adjustments;

public interface IAdjustment
{
    string Reason { get; }
    int Quantity { get; }
}