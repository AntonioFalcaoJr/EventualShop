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
            => RaiseEvent(new Events.CustomerRegistered(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new Events.CustomerAgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new Events.CustomerNameChanged(name));

        private void Apply(Events.CustomerRegistered @event)
            => (Id, Name, Age) = @event;

        private void Apply(Events.CustomerNameChanged @event)
            => Name = @event.Name;

        private void Apply(Events.CustomerAgeChanged @event)
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