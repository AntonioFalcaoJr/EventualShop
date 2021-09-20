using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using Domain.Aggregates.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers
{
    public class AccountChangedConsumer : IConsumer<Events.AccountDeleted>, IConsumer<Events.AccountUserPasswordChanged>
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

            await _projectionsService.ProjectNewAccountDetailsAsync(accountDetails, cancellationToken);
        }
    }
}