using System;

namespace Domain.Abstractions.Events
{
    public abstract record Event : IEvent
    {
        public DateTimeOffset Timestamp { get; } = DateTimeOffset.Now;
    }
}