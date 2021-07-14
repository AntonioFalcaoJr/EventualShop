using System.Collections.Generic;
using Newtonsoft.Json;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.Aggregates
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
        where TId : struct
    {
        [JsonIgnore]
        private readonly List<IEvent> _events = new();

        [JsonIgnore]
        public IEnumerable<IEvent> Events
            => _events;

        public void ClearEvents()
            => _events.Clear();

        protected void AddEvent(IEvent @event)
            => _events.Add(@event);

        protected abstract void RaiseEvent(IEvent @event);
        public abstract void Load(IEnumerable<IEvent> events);
    }
}