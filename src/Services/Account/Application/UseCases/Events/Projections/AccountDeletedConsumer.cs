using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using AccountDeletedEvent = Messages.Accounts.Events.AccountDeleted;

namespace Application.UseCases.Events.Projections
{
    public class AccountDeletedConsumer : IConsumer<AccountDeletedEvent>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountDeletedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<AccountDeletedEvent> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);
            await _projectionsService.RemoveProjectionsAsync(account.Id, context.CancellationToken);
        }
    }
}