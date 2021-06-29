using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Domain.Abstractions.Entities;
using Domain.Abstractions.Events;

namespace Domain.Abstractions.AggregateRoots
{
    public abstract class AggregateRoot<TId> : Entity<TId>, IAggregateRoot
        where TId : struct
    {
        private readonly List<IEvent> _events = new();

        public int Version { get; protected set; }

        public void RaiseEvent(IEvent @event)
        {
            Apply(@event);
            if (IsValid) _events.Add(@event);
        }

        public IEnumerable<IEvent> GetEvents()
            => _events;

        public void ClearEvents()
            => _events.Clear();

        protected void Load(IEnumerable<IEvent> events)
            => events.ToList().ForEach(Apply);

        protected void Apply(IEvent @event) =>
            GetType()
                .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(info => info.Name is nameof(Apply))
                .First(info => info.GetParameters()
                    .Any(parameterInfo => parameterInfo.ParameterType == @event.GetType()))
                .Invoke(this, new object[] {@event});
    }
}