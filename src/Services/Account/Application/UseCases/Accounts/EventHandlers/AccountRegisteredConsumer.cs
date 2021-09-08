using System.Threading.Tasks;
using Application.EventSourcing.Accounts.EventStore;
using Application.EventSourcing.Accounts.Projections;
using Domain.Entities.Accounts;
using MassTransit;

namespace Application.UseCases.Accounts.EventHandlers
{
    public class AccountRegisteredConsumer : IConsumer<Events.AccountRegistered>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountRegisteredConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.AccountRegistered> context)
        {
            var (aggregateId, _, _) = context.Message;
            
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);

            var accountDetails = new AccountDetailsProjection
            {
                Id = account.Id,
                Age = account.Age,
                Name = account.Name
            };

            await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}