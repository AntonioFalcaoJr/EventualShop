using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using UserRegisteredEvent = ECommerce.Contracts.Identity.DomainEvents.UserRegistered;
using UserPasswordChangedEvent = ECommerce.Contracts.Identity.DomainEvents.UserPasswordChanged;
using UserDeletedEvent = ECommerce.Contracts.Identity.DomainEvents.UserDeleted;

namespace Application.UseCases.Events.Projections;

public class ProjectUserDetailsWhenUserChangedConsumer :
    IConsumer<UserRegisteredEvent>,
    IConsumer<UserPasswordChangedEvent>,
    IConsumer<UserDeletedEvent>
{
    private readonly IUserEventStoreService _eventStoreService;
    private readonly IUserProjectionsService _projectionsService;

    public ProjectUserDetailsWhenUserChangedConsumer(IUserEventStoreService eventStoreService, IUserProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<UserDeletedEvent> context)
        => ProjectAsync(context.Message.UserId, context.CancellationToken);

    public Task Consume(ConsumeContext<UserPasswordChangedEvent> context)
        => ProjectAsync(context.Message.UserId, context.CancellationToken);

    public Task Consume(ConsumeContext<UserRegisteredEvent> context)
        => ProjectAsync(context.Message.UserId, context.CancellationToken);

    private async Task ProjectAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await _eventStoreService.LoadAggregateFromStreamAsync(userId, cancellationToken);

        var userAuthenticationDetails = new UserAuthenticationDetailsProjection
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            IsDeleted = user.IsDeleted
        };

        await _projectionsService.ProjectAsync(userAuthenticationDetails, cancellationToken);
    }
}