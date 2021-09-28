using System;
using Domain.Abstractions.Aggregates;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.Profiles;
using Messages.Abstractions.Events;
using Messages.Accounts;

namespace Domain.Aggregates
{
    public class Account : AggregateRoot<Guid>
    {
        public Guid UserId { get; private set; }
        public Profile Profile { get; private set; }

        public void Create(Guid userId, string email, string firstName)
            => RaiseEvent(new Events.AccountCreated(Guid.NewGuid(), userId, email, firstName));

        public void Delete(Guid id)
            => RaiseEvent(new Events.AccountDeleted(id));

        public void UpdateProfile(Guid id, DateOnly birthdate, string email, string firstName, string lastName)
            => RaiseEvent(new Events.ProfileUpdated(id, birthdate, email, firstName, lastName));

        public void DefineResidenceAddress(Guid id, string city, string country, int? number, string state, string street, string zipCode)
            => RaiseEvent(new Events.ResidenceAddressDefined(id, city, country, number, state, street, zipCode));

        public void DefineProfessionalAddress(Guid id, string city, string country, int? number, string state, string street, string zipCode)
            => RaiseEvent(new Events.ProfessionalAddressDefined(id, city, country, number, state, street, zipCode));

        protected override void ApplyEvent(IEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.AccountCreated @event)
        {
            Id = @event.Id;
            UserId = @event.UserId;
            Profile = new Profile(@event.Email, @event.FirstName);
        }

        private void When(Events.AccountDeleted _)
            => IsDeleted = true;

        private void When(Events.ProfileUpdated @event)
        {
            Profile.SetBirthdate(@event.Birthdate);
            Profile.SetEmail(@event.Email);
            Profile.SetFirstName(@event.FirstName);
            Profile.SetLastName(@event.LastName);
        }

        private void When(Events.ResidenceAddressDefined @event)
        {
            var address = new Address(
                @event.City,
                @event.Country,
                @event.Number,
                @event.State,
                @event.Street,
                @event.ZipCode);

            Profile.SetResidenceAddress(address);
        }

        private void When(Events.ProfessionalAddressDefined @event)
        {
            var address = new Address(
                @event.City,
                @event.Country,
                @event.Number,
                @event.State,
                @event.Street,
                @event.ZipCode);

            Profile.SetProfessionalAddress(address);
        }

        protected sealed override bool Validate()
            => OnValidate<AccountValidator, Account>();
    }
}