using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.EventHandlers.Projections
{
    public class AccountCreatedConsumer : IConsumer<Events.AccountCreated>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountCreatedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.AccountCreated> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.Id, context.CancellationToken);

            var accountDetails = new AccountDetailsProjection
            {
                Id = account.Id,
                UserId = account.UserId,
                Profile = new ProfileProjection
                {
                    Email = account.Profile.Email,
                    FirstName = account.Profile.FirstName,
                },
                IsDeleted = account.IsDeleted
            };

            await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, context.CancellationToken);
        }
    }
}