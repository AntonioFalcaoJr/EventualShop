using System;
using System.Collections.Generic;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public class Customer : Aggregate<Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void Register(string name, int age)
            => RaiseEvent(new Events.Registered(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new Events.AgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new Events.NameChanged(name));

        private void Apply(Events.Registered @event)
            => (Id, Name, Age) = @event;

        private void Apply(Events.NameChanged @event)
            => Name = @event.Name;

        private void Apply(Events.AgeChanged @event)
            => Age = @event.Age;

        protected override void RaiseEvent(IEvent @event)
        {
            Apply((dynamic) @event);
            if (IsValid) AddEvent(@event);
        }

        public override void Load(IEnumerable<IEvent> events)
        {
            foreach (var @event in events)
                Apply((dynamic) @event);
        }

        protected sealed override bool Validate()
            => OnValidate<Validator, Customer>(this, new());
    }
}