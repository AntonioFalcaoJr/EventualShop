using System;
using Messages.Accounts.Commands;

namespace ECommerce.WebAPI.Messages.Accounts
{
    public static class Commands
    {
        public record AddNewAccountOwnerAddressCommand(Guid AccountId, Guid OwnerId, string City, string Country, int? Number, string State, string Street, string ZipCode) : AddNewAccountOwnerAddress;

        public record AddNewAccountOwnerCreditCardCommand(Guid AccountId, Guid OwnerId, Guid WalletId, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : AddNewAccountOwnerCreditCard;

        public record ChangeAccountPasswordCommand(Guid AccountId, Guid UserId, string NewPassword, string NewPasswordConfirmation) : ChangeAccountPassword;

        public record DefineAccountOwnerCommand(Guid AccountId, int Age, string Email, string LastName, string Name) : DefineAccountOwner;

        public record DeleteAccountCommand(Guid AccountId) : DeleteAccount;

        public record RegisterAccountCommand(string Password, string PasswordConfirmation, string UserName) : RegisterAccount;

        public record UpdateAccountOwnerAddressCommand(Guid AccountId, Guid OwnerId, string City, string Country, int? Number, string State, string Street, string ZipCode) : UpdateAccountOwnerAddress;

        public record UpdateAccountOwnerCreditCardCommand(Guid AccountId, Guid OwnerId, Guid WalletId, DateOnly Expiration, string HolderName, string Number, string SecurityNumber) : UpdateAccountOwnerCreditCard;

        public record UpdateAccountOwnerDetailsCommand(Guid AccountId, Guid OwnerId, int Age, string Email, string LastName, string Name) : UpdateAccountOwnerDetails;
    }
}