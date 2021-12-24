using System;

namespace Domain.Entities;

public class Reserve
{
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public DateTimeOffset ReservedAt { get; } = DateTimeOffset.Now;
}