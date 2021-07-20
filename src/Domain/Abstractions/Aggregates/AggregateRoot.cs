using System.Collections.Generic;
using Newtonsoft.Json;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.Aggregates
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
        where TId : struct
    {
        [JsonIgnore]
        private readonly List<IDomainEvent> _events = new();

        [JsonIgnore]
        public IEnumerable<IDomainEvent> Events
            => _events;

        public void ClearEvents()
            => _events.Clear();

        protected void AddEvent(IDomainEvent @event)
            => _events.Add(@event);

        protected abstract void RaiseEvent(IDomainEvent @event);
        public abstract void Load(IEnumerable<IDomainEvent> events);
    }
}