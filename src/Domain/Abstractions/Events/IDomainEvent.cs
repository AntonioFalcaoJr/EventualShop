using System;

namespace Domain.Abstractions.Events
{
    public interface IDomainEvent
    {
        public DateTimeOffset Timestamp { get; }
    }
}