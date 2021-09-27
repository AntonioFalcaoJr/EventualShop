using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.CommandHandlers
{
    public class UpdateProfileConsumer : IConsumer<Commands.UpdateProfile>
    {
        private readonly IAccountEventStoreService _eventStoreService;

        public UpdateProfileConsumer(IAccountEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task Consume(ConsumeContext<Commands.UpdateProfile> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);

            if (account is null)
            {
                // TODO - Notification
                return;
            }

            account.UpdateProfile(
                account.Id,
                context.Message.Birthdate,
                context.Message.Email,
                context.Message.FirstName,
                context.Message.LastName);

            await _eventStoreService.AppendEventsToStreamAsync(account, context.CancellationToken);
        }
    }
}