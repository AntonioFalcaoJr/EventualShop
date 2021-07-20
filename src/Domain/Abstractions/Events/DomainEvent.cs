using System;

namespace Domain.Abstractions.Events
{
    public abstract record DomainEvent : IDomainEvent
    {
        public DateTimeOffset Timestamp { get; private set; } = DateTimeOffset.Now;
    }
}