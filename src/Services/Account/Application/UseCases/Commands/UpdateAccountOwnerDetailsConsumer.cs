using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class UpdateAccountOwnerDetailsConsumer : IConsumer<UpdateAccountOwnerDetails>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public UpdateAccountOwnerDetailsConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateAccountOwnerDetails> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            if (account.Owner.Id != context.Message.OwnerId)
            {
                // TODO - Notification
                return;
            }
            
            account.UpdateOwnerDetails(
                account.Id,
                account.Owner.Id,
                context.Message.Age,
                context.Message.Email,
                context.Message.LastName,
                context.Message.Name);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}