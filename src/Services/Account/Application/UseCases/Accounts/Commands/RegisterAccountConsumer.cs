using System.Threading.Tasks;
using Application.EventSourcing.Accounts.EventStore;
using Domain.Entities.Accounts;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Accounts.Commands
{
    public class RegisterAccountConsumer : IConsumer<RegisterAccount>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public RegisterAccountConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<RegisterAccount> context)
        {
            var account = new Account();
            account.Register(context.Message.Name, context.Message.Age);
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}