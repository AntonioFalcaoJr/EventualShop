using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.ValueObjects.CreditCards;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class AddNewAccountOwnerAddressConsumer : IConsumer<AddNewAccountOwnerCreditCard>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public AddNewAccountOwnerAddressConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddNewAccountOwnerCreditCard> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            if (account.Owner.Id != context.Message.OwnerId)
            {
                // TODO - Notification
                return;
            }
            
            if (account.Owner.Wallet.Id != context.Message.WalletId)
            {
                // TODO - Notification
                return;
            }

            var creditCard = new CreditCard(
                context.Message.Expiration,
                context.Message.HolderName,
                context.Message.Number,
                context.Message.SecurityNumber);

            account.AddNewOwnerCreditCard(account.Id, account.Owner.Id, creditCard);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}