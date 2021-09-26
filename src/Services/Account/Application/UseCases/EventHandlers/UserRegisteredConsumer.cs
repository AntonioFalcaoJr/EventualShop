using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates.Accounts;
using Domain.Entities.Users;
using MassTransit;
using Events = Messages.Identities.Events;

namespace Application.UseCases.EventHandlers
{
    public class UserRegisteredConsumer : IConsumer<Events.UserRegistered>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public UserRegisteredConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Events.UserRegistered> context)
        {
            var account = new Account();

            var user = new User(
                "context.Message.Password",
                "context.Message.Password",
                "test");

            account.RegisterUser(user);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}