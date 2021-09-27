using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Identities;

namespace Application.UseCases.EventHandlers
{
    public class UserChangedConsumer :
        IConsumer<Events.UserRegistered>,
        IConsumer<Events.UserPasswordChanged>,
        IConsumer<Events.UserDeleted>

    {
        private readonly IUserEventStoreService _eventStoreService;
        private readonly IUserProjectionsService _projectionsService;

        public UserChangedConsumer(IUserEventStoreService eventStoreService, IUserProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public Task Consume(ConsumeContext<Events.UserRegistered> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.UserPasswordChanged> context)
            => Project(context.Message.Id, context.CancellationToken);

        public Task Consume(ConsumeContext<Events.UserDeleted> context)
            => Project(context.Message.Id, context.CancellationToken);

        private async Task Project(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _eventStoreService.LoadAggregateFromStreamAsync(userId, cancellationToken);

            var userAuthenticationDetails = new UserAuthenticationDetailsProjection
            {
                Id = user.Id,
                Password = user.Password,
                IsDeleted = user.IsDeleted,
                Login = user.FirstName
            };

            await _projectionsService.ProjectUserAuthenticationDetailsAsync(userAuthenticationDetails, cancellationToken);
        }
    }
}