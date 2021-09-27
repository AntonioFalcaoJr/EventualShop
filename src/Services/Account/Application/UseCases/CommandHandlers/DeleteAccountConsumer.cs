using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.CommandHandlers
{
    public class DeleteAccountConsumer : IConsumer<Commands.DeleteAccount>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public DeleteAccountConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.DeleteAccount> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
            account.Delete(account.Id);
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}