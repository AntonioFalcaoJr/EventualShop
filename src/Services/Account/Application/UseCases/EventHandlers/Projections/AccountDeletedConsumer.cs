using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.EventHandlers.Projections
{
    public class AccountDeletedConsumer : IConsumer<Events.AccountDeleted>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountDeletedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.AccountDeleted> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);
            await _projectionsService.RemoveProjectionsAsync(account.Id, context.CancellationToken);
        }
    }
}