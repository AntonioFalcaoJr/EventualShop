using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.AggregateRoots
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : struct
    {
        private readonly List<IEvent> _events = new();

        protected void Load(IEnumerable<IEvent> events)
            => events.ToList().ForEach(ApplyChanges);

        protected abstract void ApplyChanges(IEvent @event);

        protected void AddEvent(IEvent @event)
            => _events.Add(@event);

        protected List<IEvent> GetEvents()
            => _events;
    }
}