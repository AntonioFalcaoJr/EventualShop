using System;
using Domain.Abstractions.Events;
using Domain.Entities.Owners;
using Domain.Entities.Users;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.CreditCards;

namespace Domain.Aggregates.Accounts
{
    public static class Events
    {
        public record AccountOwnerDetailsUpdated(Guid AccountId, Guid OwnerId, int Age, string Email, string LastName, string Name) : DomainEvent;

        public record AccountOwnerDefined(Guid AccountId, Owner Owner) : DomainEvent;

        public record AccountOwnerNewAddressAdded(Guid AccountId, Guid OwnerId, Address Address) : DomainEvent;

        public record AccountOwnerNewCreditCardAdded(Guid AccountId, Guid UserId, Guid WalletId, CreditCard CreditCard) : DomainEvent;

        public record AccountDeleted(Guid AccountId) : DomainEvent;

        public record AccountUserRegistered(Guid AccountId, User User) : DomainEvent;

        public record AccountUserPasswordChanged(Guid AccountId, Guid UserId, string NewPassword, string NewPasswordConfirmation) : DomainEvent;

        public record AccountOwnerAddressUpdated(Guid AccountId, Guid OwnerId, Address Address) : DomainEvent;

        public record AccountOwnerCreditCardUpdated(Guid AccountId, Guid OwnerId, Guid WalletId, CreditCard CreditCard) : DomainEvent;
    }
}