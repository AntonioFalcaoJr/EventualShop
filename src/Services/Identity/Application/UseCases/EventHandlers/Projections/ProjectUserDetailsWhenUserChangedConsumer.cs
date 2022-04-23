using Application.Abstractions.Projections;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectUserDetailsWhenUserChangedConsumer :
    IConsumer<DomainEvents.UserRegistered>,
    IConsumer<DomainEvents.UserPasswordChanged>,
    IConsumer<DomainEvents.UserDeleted>
{
    private readonly IProjectionRepository<ECommerce.Contracts.Identities.Projections.UserAuthentication> _repository;

    public ProjectUserDetailsWhenUserChangedConsumer(IProjectionRepository<ECommerce.Contracts.Identities.Projections.UserAuthentication> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvents.UserDeleted> context)
        => await _repository.DeleteAsync(context.Message.UserId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.UserPasswordChanged> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.UserId,
            field: user => user.Password,
            value: context.Message.NewPassword,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvents.UserRegistered> context)
    {
        var userAuthentication = new ECommerce.Contracts.Identities.Projections.UserAuthentication
        {
            Id = context.Message.UserId,
            Email = context.Message.Email,
            Password = context.Message.Password,
            IsDeleted = false
        };

        await _repository.InsertAsync(userAuthentication, context.CancellationToken);
    }
}