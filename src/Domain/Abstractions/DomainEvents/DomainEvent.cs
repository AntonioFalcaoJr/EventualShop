using System;

namespace Domain.Abstractions.DomainEvents
{
    public abstract record DomainEvent : IDomainEvent
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}