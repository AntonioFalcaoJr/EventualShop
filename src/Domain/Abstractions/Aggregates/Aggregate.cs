using System.Collections.Generic;
using Domain.Abstractions.DomainEvents;
using Domain.Abstractions.Entities;

namespace Domain.Abstractions.Aggregates
{
    public abstract class Aggregate<TDomainEvent, TId> : Entity<TId>, IAggregate<TDomainEvent, TId>
        where TDomainEvent : IDomainEvent
        where TId : struct
    {
        private readonly List<TDomainEvent> _events = new();
        public int Version { get; protected set; }

        public IReadOnlyCollection<TDomainEvent> Events
            => _events;

        public void ClearEvents()
            => _events.Clear();

        protected void AddEvent(TDomainEvent @event)
            => _events.Add(@event);

        protected abstract void RaiseEvent(TDomainEvent @event);
        public abstract void Load(IEnumerable<TDomainEvent> events);
    }
}