using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Entities.Events;

namespace Domain.Abstractions.Entities.AggregateRoots
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : struct
    {
        protected List<IEvent> Events = new();

        protected void Load(IEnumerable<IEvent> events) 
            => events.ToList().ForEach(Apply);

        protected abstract void Apply(IEvent @event);
    }
}