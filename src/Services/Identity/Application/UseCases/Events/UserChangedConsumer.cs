using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using UserRegisteredEvent = Messages.Identities.Events.UserRegistered;
using UserPasswordChangedEvent = Messages.Identities.Events.UserPasswordChanged;
using UserDeletedEvent = Messages.Identities.Events.UserDeleted;

namespace Application.UseCases.Events
{
    public class UserChangedConsumer :
        IConsumer<UserRegisteredEvent>,
        IConsumer<UserPasswordChangedEvent>,
        IConsumer<UserDeletedEvent>
    {
        private readonly IUserEventStoreService _eventStoreService;
        private readonly IUserProjectionsService _projectionsService;

        public UserChangedConsumer(IUserEventStoreService eventStoreService, IUserProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public Task Consume(ConsumeContext<UserRegisteredEvent> context)
            => Project(context.Message.UserId, context.CancellationToken);

        public Task Consume(ConsumeContext<UserPasswordChangedEvent> context)
            => Project(context.Message.UserId, context.CancellationToken);

        public Task Consume(ConsumeContext<UserDeletedEvent> context)
            => Project(context.Message.UserId, context.CancellationToken);

        private async Task Project(Guid userId, CancellationToken cancellationToken)
        {
            var user = await _eventStoreService.LoadAggregateFromStreamAsync(userId, cancellationToken);

            var userAuthenticationDetails = new UserAuthenticationDetailsProjection
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                IsDeleted = user.IsDeleted
            };

            await _projectionsService.ProjectUserAuthenticationDetailsAsync(userAuthenticationDetails, cancellationToken);
        }
    }
}