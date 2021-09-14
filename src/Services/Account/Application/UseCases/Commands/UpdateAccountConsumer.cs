using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
{
    public class UpdateAccountConsumer : IConsumer<UpdateAccount>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public UpdateAccountConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<UpdateAccount> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);

            account.ChangeAge(context.Message.Age);
            account.ChangeName(context.Message.Name);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}