using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Events
{
    public class UserRegisteredConsumer : IConsumer<Messages.Identities.Events.UserRegistered>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public UserRegisteredConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Messages.Identities.Events.UserRegistered> context)
        {
            var account = new Account();
            account.Create(context.Message.UserId, context.Message.Email, context.Message.FirstName);
            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}