using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using  ECommerce.Contracts.Identities;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectUserDetailsWhenUserChangedConsumer :
    IConsumer<DomainEvents.UserRegistered>,
    IConsumer<DomainEvents.UserPasswordChanged>,
    IConsumer<DomainEvents.UserDeleted>
{
    private readonly IUserEventStoreService _eventStoreService;
    private readonly IUserProjectionsService _projectionsService;

    public ProjectUserDetailsWhenUserChangedConsumer(IUserEventStoreService eventStoreService, IUserProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.UserDeleted> context)
        => ProjectAsync(context.Message.UserId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.UserPasswordChanged> context)
        => ProjectAsync(context.Message.UserId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.UserRegistered> context)
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