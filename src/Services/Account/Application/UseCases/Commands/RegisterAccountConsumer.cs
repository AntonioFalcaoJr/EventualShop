using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Entities.Accounts;
using Domain.Entities.Users;
using MassTransit;
using Messages.Accounts.Commands;

namespace Application.UseCases.Commands
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

            var user = new User(
                context.Message.Password, 
                context.Message.PasswordConfirmation, 
                context.Message.UserName);

            account.RegisterUser(user);
            
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}