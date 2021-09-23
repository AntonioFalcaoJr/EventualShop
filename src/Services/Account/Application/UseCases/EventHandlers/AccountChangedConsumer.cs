using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Aggregates.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class AccountChangedConsumer :
        IConsumer<Events.AccountDeleted>,
        IConsumer<Events.AccountUserPasswordChanged>,
        IConsumer<Events.AccountOwnerDefined>,
        IConsumer<Events.AccountOwnerNewAddressAdded>,
        IConsumer<Events.AccountOwnerNewCreditCardAdded>,
        IConsumer<Events.AccountOwnerAddressUpdated>,
        IConsumer<Events.AccountOwnerCreditCardUpdated>,
        IConsumer<Events.AccountOwnerDetailsUpdated>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public AccountChangedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public Task Consume(ConsumeContext<Events.AccountDeleted> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountUserPasswordChanged> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountOwnerDefined> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountOwnerNewAddressAdded> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountOwnerNewCreditCardAdded> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountOwnerAddressUpdated> context)
            => Project(context.Message.AccountId, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.AccountOwnerCreditCardUpdated> context)
            => Project(context.Message.AccountId, context.CancellationToken);
        
        public Task Consume(ConsumeContext<Events.AccountOwnerDetailsUpdated> context)
            => Project(context.Message.AccountId, context.CancellationToken);
        
        private async Task Project(Guid accountId, CancellationToken cancellationToken)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(accountId, cancellationToken);

            var accountDetails = new AccountAuthenticationDetailsProjection
            {
                Id = account.Id,
                UserId = account.User.Id,
                Password = account.User.Password,
                IsDeleted = account.IsDeleted,
                UserName = account.User.Name
            };

            await _projectionsService.UpdateAccountDetailsAsync(accountDetails, cancellationToken);
        }
    }
}