using System;
using Domain.Abstractions.Aggregates;
using Domain.Abstractions.Events;
using Domain.Entities.Owners;
using Domain.Entities.Users;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;

namespace Domain.Aggregates.Accounts
{
    public class Account : AggregateRoot<Guid>
    {
        public Owner Owner { get; private set; }
        public User User { get; private set; }

        public void RegisterUser(User user)
            => RaiseEvent(new Events.AccountUserRegistered(Guid.NewGuid(), user));

        public void ChangePassword(Guid accountId, Guid userId, string newPassword, string newPasswordConfirmation)
            => RaiseEvent(new Events.AccountUserPasswordChanged(accountId, userId, newPassword, newPasswordConfirmation));

        public void Delete(Guid accountId)
            => RaiseEvent(new Events.AccountDeleted(accountId));

        public void DefineOwner(Guid accountId, Owner owner)
            => RaiseEvent(new Events.AccountOwnerDefined(accountId, owner));

        public void UpdateOwnerDetails(Guid accountId, Guid ownerId, int age, string email, string lastName, string name)
            => RaiseEvent(new Events.AccountOwnerDetailsUpdated(accountId, ownerId, age, email, lastName, name));

        public void AddNewOwnerAddress(Guid accountId, Guid ownerId, Address address)
            => RaiseEvent(new Events.AccountOwnerNewAddressAdded(accountId, ownerId, address));

        public void AddNewOwnerCreditCard(Guid accountId, Guid ownerId, Guid walletId, CreditCard creditCard)
            => RaiseEvent(new Events.AccountOwnerNewCardAdded(accountId, ownerId, walletId, creditCard));

        public void UpdateOwnerAddress(Guid accountId, Guid ownerId, Address address)
            => RaiseEvent(new Events.AccountOwnerAddressUpdated(accountId, ownerId, address));

        public void UpdateOwnerCreditCard(Guid accountId, Guid ownerId, Guid walletId, CreditCard creditCard)
            => RaiseEvent(new Events.AccountOwnerCreditCardUpdated(accountId, ownerId, walletId, creditCard));

        protected override void ApplyEvent(IDomainEvent domainEvent)
            => When(domainEvent as dynamic);

        private void When(Events.AccountUserRegistered @event)
            => (Id, User) = @event;

        private void When(Events.AccountUserPasswordChanged @event)
            => User.ChangePassword(@event.NewPassword, @event.NewPasswordConfirmation);

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

        private void When(Events.AccountOwnerCreditCardUpdated @event)
        {
            Owner.RemoveCreditCard(@event.CreditCard);
            Owner.AddNewCreditCard(@event.CreditCard);
        }

        protected sealed override bool Validate()
            => OnValidate<AccountValidator, Account>();
    }
}