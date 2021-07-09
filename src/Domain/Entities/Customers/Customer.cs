using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstractions.Aggregates;

namespace Domain.Entities.Customers
{
    public class Customer : Aggregate<CustomerDomainEvent, Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void Register(string name, int age)
            => RaiseEvent(new CustomerRegisteredDomainEvent(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new CustomerAgeChangedDomainEvent(age));

        public void ChangeName(string name)
            => RaiseEvent(new CustomerNameChangedDomainEvent(name));

        private void Apply(CustomerRegisteredDomainEvent @event)
            => (Id, Name, Age) = @event;

        private void Apply(CustomerNameChangedDomainEvent @event)
            => Name = @event.Name;

        private void Apply(CustomerAgeChangedDomainEvent @event)
            => Age = @event.Age;

        protected override void RaiseEvent(CustomerDomainEvent domainEvent)
        {
            Apply(domainEvent as dynamic);
            if (IsValid) AddEvent(domainEvent);
        }
        
        public override void Load(IEnumerable<CustomerDomainEvent> events)
            => events.ToList().ForEach(@event => Apply(@event as dynamic));

        protected sealed override bool Validate()
            => OnValidate<CustomerValidator, Customer>(this, new());
    }
}