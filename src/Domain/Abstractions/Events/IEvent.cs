using System;

namespace Domain.Abstractions.Events
{
    public interface IEvent
    {
        public DateTimeOffset Timestamp { get; }
    }
}