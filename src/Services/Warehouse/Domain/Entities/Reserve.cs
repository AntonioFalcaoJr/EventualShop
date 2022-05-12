namespace Domain.Entities;

public class Reserve
{
    public Guid CartId { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset ReservedAt { get; } = DateTimeOffset.Now;
}