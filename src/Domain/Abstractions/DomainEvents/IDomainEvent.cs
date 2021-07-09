using System;

namespace Domain.Abstractions.DomainEvents
{
    public interface IDomainEvent
    {
        public DateTimeOffset Timestamp { get; }
    }
}