using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.ValueObjects.Addresses;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class AddNewAccountOwnerCreditCardConsumer : IConsumer<AddNewAccountOwnerAddress>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public AddNewAccountOwnerCreditCardConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<AddNewAccountOwnerAddress> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            if (account.Owner.Id != context.Message.OwnerId)
            {
                // TODO - Notification
                return;
            }

            var address = new Address(
                context.Message.City,
                context.Message.Country,
                context.Message.Number,
                context.Message.State,
                context.Message.Street,
                context.Message.ZipCode);

            account.AddNewOwnerAddress(account.Id, account.Owner.Id, address);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}