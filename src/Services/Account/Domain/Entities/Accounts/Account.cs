using System;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;

namespace Domain.Entities.Accounts
{
    public class Account : AggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public void Register(string name, int age)
            => RaiseEvent(new Events.AccountRegistered(Guid.NewGuid(), name, age));

        public void ChangeAge(int age)
            => RaiseEvent(new Events.AccountAgeChanged(age));

        public void ChangeName(string name)
            => RaiseEvent(new Events.AccountNameChanged(name));

        protected override void ApplyEvent(IDomainEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.AccountRegistered @event)
            => (Id, Name, Age) = @event;

        private void When(Events.AccountNameChanged @event)
            => Name = @event.Name;

        private void When(Events.AccountAgeChanged @event)
            => Age = @event.Age;

        protected sealed override bool Validate()
            => OnValidate<Validator, Account>();
    }
}