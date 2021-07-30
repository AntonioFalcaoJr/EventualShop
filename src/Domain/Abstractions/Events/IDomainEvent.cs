using System;

namespace Domain.Abstractions.Events
{
    public interface IDomainEvent
    {
        DateTimeOffset Timestamp { get; }
        int AggregateVersion { get; set; }
    }
}