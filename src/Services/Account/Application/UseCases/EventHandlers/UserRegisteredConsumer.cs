using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;
using Messages.Identities;

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
            account.Create(context.Message.UserId, context.Message.Email, context.Message.FirstName);
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}