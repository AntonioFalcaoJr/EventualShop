using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public class Customer : Aggregate<Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void Register(string name, int age)
            => RaiseEvent(new CustomerEvents.Registered(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new CustomerEvents.AgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new CustomerEvents.NameChanged(name));

        private void Apply(CustomerEvents.Registered @event)
            => (Id, Name, Age) = @event;

        private void Apply(CustomerEvents.NameChanged @event)
            => Name = @event.Name;

        private void Apply(CustomerEvents.AgeChanged @event)
            => Age = @event.Age;

        protected override void RaiseEvent(IEvent @event)
        {
            Apply(@event as dynamic);
            if (IsValid) AddEvent(@event);
        }
        
        public override void Load(IEnumerable<IEvent> events)
            => events.ToList().ForEach(@event => Apply(@event as dynamic));

        protected sealed override bool Validate()
            => OnValidate<CustomerValidator, Customer>(this, new());
    }
}