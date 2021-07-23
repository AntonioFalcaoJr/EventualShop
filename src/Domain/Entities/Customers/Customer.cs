using System;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Domain.Entities.Customers
{
    public class Customer : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void Register(string name, int age)
            => RaiseEvent(new Events.CustomerRegistered(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new Events.CustomerAgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new Events.CustomerNameChanged(name));

        protected override void Apply(IDomainEvent @event)
            => When(@event as dynamic);

        private void When(Events.CustomerRegistered @event)
            => (Id, Name, Age) = @event;

        private void When(Events.CustomerNameChanged @event)
            => Name = @event.Name;

        private void When(Events.CustomerAgeChanged @event)
            => Age = @event.Age;

        protected sealed override bool Validate()
            => OnValidate<Validator, Customer>();
    }
}