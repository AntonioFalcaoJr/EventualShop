using System;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Domain.Entities.Owners;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.Cards;

namespace Domain.Entities.Accounts
{
    public class Account : AggregateRoot<Guid>
    {
        public Owner Owner { get; private set; }
        public string Password { get; private set; }
        public string PasswordConfirmation { get; private set; }
        public string UserName { get; private set; }

        public void Register(string password, string passwordConfirmation, string userName)
            => RaiseEvent(new Events.AccountRegistered(Guid.NewGuid(), userName, password, passwordConfirmation));

        public void ChangePassword(Guid accountId, string newPassword, string newPasswordConfirmation)
            => RaiseEvent(new Events.AccountPasswordChanged(accountId, newPassword, newPasswordConfirmation));

        public void Delete(Guid accountId)
            => RaiseEvent(new Events.AccountDeleted(accountId));

        public void DefineOwner(Guid accountId, Owner owner)
            => RaiseEvent(new Events.AccountOwnerDefined(accountId, owner));

        public void UpdateOwnerDetails(Guid accountId, Guid ownerId, string name, string lastName, int age, string email)
            => RaiseEvent(new Events.AccountOwnerDetailsUpdated(accountId, ownerId, name, lastName, age, email));

        public void AddNewOwnerAddress(Guid accountId, Guid ownerId, Address address)
            => RaiseEvent(new Events.AccountOwnerNewAddressAdded(accountId, ownerId, address));

        public void AddNewOwnerCard(Guid accountId, Guid ownerId, CreditCard creditCard)
            => RaiseEvent(new Events.AccountOwnerNewCardAdded(accountId, ownerId, creditCard));

        public void UpdateOwnerAddress(Guid accountId, Guid ownerId, Address address)
            => RaiseEvent(new Events.AccountOwnerAddressUpdated(accountId, ownerId, address));

        public void UpdateOwnerCard(Guid accountId, Guid ownerId, Guid walletId, CreditCard creditCard)
            => RaiseEvent(new Events.AccountOwnerCardUpdated(accountId, ownerId, walletId, creditCard));

        protected override void ApplyEvent(IDomainEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.AccountRegistered @event)
            => (Id, UserName, Password, PasswordConfirmation) = @event;

        private void When(Events.AccountPasswordChanged @event)
            => (_, Password, PasswordConfirmation) = @event;

        private void When(Events.AccountDeleted _)
            => IsDeleted = true;

        private void When(Events.AccountOwnerDefined @event)
            => Owner = @event.Owner;

        private void When(Events.AccountOwnerDetailsUpdated @event)
        {
            Owner.SetAge(@event.Age);
            Owner.SetEmail(@event.Email);
            Owner.SetLastName(@event.LastName);
            Owner.SetName(@event.Name);
        }

        private void When(Events.AccountOwnerNewAddressAdded @event)
            => Owner.AddNewAddress(@event.Address);

        private void When(Events.AccountOwnerNewCardAdded @event)
            => Owner.AddNewCreditCard(@event.CreditCard);

        private void When(Events.AccountOwnerAddressUpdated @event)
        {
            Owner.RemoveAddress(@event.Address);
            Owner.AddNewAddress(@event.Address);
        }

        private void When(Events.AccountOwnerCardUpdated @event)
        {
            Owner.RemoveCreditCard(@event.CreditCard);
            Owner.AddNewCreditCard(@event.CreditCard);
        }

        protected sealed override bool Validate()
            => OnValidate<AccountValidator, Account>();
    }
}