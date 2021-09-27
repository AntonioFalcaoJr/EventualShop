using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.CommandHandlers
{
    public class CreateAccountConsumer : IConsumer<Commands.CreateAccount>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public CreateAccountConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.CreateAccount> context)
        {
            var account = new Account();
            account.Create(context.Message.AccountId, context.Message.Email, context.Message.FirstName);
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}