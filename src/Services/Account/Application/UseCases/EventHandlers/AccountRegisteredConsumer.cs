using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Entities.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers
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
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            var accountDetails = new AccountDetailsProjection
            {
                Id = account.Id,
                Password = account.Password,
                IsDeleted = account.IsDeleted,
                UserName = account.UserName
            };

            await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}