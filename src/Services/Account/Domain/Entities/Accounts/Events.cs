using System;
using Domain.Abstractions.Events;
using Domain.Entities.Owners;
using Domain.ValueObjects.Addresses;
using Domain.ValueObjects.Cards;

namespace Domain.Entities.Accounts
{
    public static class Events
    {
        public record AccountOwnerDetailsUpdated(Guid AccountId, Guid OwnerId, string Name, string LastName, int Age, string Email) : DomainEvent;

        public record AccountOwnerDefined(Guid AccountId, Owner Owner) : DomainEvent;

        public record AccountOwnerNewAddressAdded(Guid AccountId, Guid UserId, Address Address) : DomainEvent;

        public record AccountOwnerNewCardAdded(Guid AccountId, Guid UserId, CreditCard CreditCard) : DomainEvent;

        public record AccountDeleted(Guid AccountId) : DomainEvent;

        public record AccountRegistered(Guid AccountId, string UserName, string Password, string PasswordConfirmation) : DomainEvent;

        public record AccountPasswordChanged(Guid AccountId, string NewPassword, string NewPasswordConfirmation) : DomainEvent;

        public record AccountOwnerAddressUpdated(Guid AccountId, Guid OwnerId, Address Address) : DomainEvent;

        public record AccountOwnerCardUpdated(Guid AccountId, Guid OwnerId, Guid WalletId, CreditCard CreditCard) : DomainEvent;
    }
}