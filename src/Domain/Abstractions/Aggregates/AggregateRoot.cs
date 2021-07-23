using System.Collections.Generic;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;
using Newtonsoft.Json;

namespace Domain.Abstractions.Aggregates
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot<TId>
        where TId : struct
    {
        [JsonIgnore]
        private readonly List<IDomainEvent> _events = new();

        public int Version { get; private set; }

        [JsonIgnore]
        public IEnumerable<IDomainEvent> Events
            => _events;

        public void Load(IEnumerable<IDomainEvent> events)
        {
            foreach (var @event in events)
                Apply((dynamic) @event);
        }

        public void ClearEvents()
            => _events.Clear();

        private void AddEvent(IDomainEvent @event)
            => _events.Add(@event);

        private void IncreaseVersion()
            => Version++;

        protected abstract void Apply(IDomainEvent @event);

        protected void RaiseEvent(IDomainEvent @event)
        {
            Apply((dynamic) @event);
            if (IsValid is false) return;
            AddEvent(@event);
            IncreaseVersion();
        }
    }
}