using System;
using Domain.Abstractions.AggregateRoots;
using Domain.Entities.Customers.Events;

namespace Domain.Entities.Customers
{
    public class Customer : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Customer(string fieldString, int fieldNumber)
            => RaiseEvent(new CustomerCreated(fieldString, fieldNumber));

        public void ChangeAge(int age)
            => RaiseEvent(new CustomerAgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new CustomerNameChanged(name));

        private void Apply(CustomerCreated @event)
            => (Name, Age) = @event;

        private void Apply(CustomerNameChanged @event)
            => Name = @event.Name;

        private void Apply(CustomerAgeChanged @event)
            => Age = @event.Age;

        protected sealed override bool Validate()
            => OnValidate<CustomerValidator, Customer>(this, new());
    }
}