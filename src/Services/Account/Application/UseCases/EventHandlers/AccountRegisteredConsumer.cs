using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Entities.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class AccountRegisteredConsumer : IConsumer<Events.AccountUserRegistered>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountRegisteredConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.AccountUserRegistered> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            var accountDetails = new AccountAuthenticationDetailsProjection
            {
                Id = account.Id,
                UserId = account.User.Id,
                Password = account.User.Password,
                IsDeleted = account.IsDeleted,
                UserName = account.User.Name
            };

            await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}