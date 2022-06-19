using Application.Abstractions.Projections;
using Contracts.Services.Identity;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectUserDetailsWhenUserChangedConsumer :
    IConsumer<DomainEvent.UserRegistered>,
    IConsumer<DomainEvent.UserPasswordChanged>,
    IConsumer<DomainEvent.UserDeleted>
{
    private readonly IProjectionRepository<Projection.UserAuthentication> _repository;

    public ProjectUserDetailsWhenUserChangedConsumer(IProjectionRepository<Projection.UserAuthentication> repository)
        => _repository = repository;

    public Task Consume(ConsumeContext<DomainEvent.UserDeleted> context)
        => _repository.DeleteAsync(context.Message.UserId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserPasswordChanged> context)
        => _repository.UpdateFieldAsync(
            id: context.Message.UserId,
            field: user => user.Password,
            value: context.Message.NewPassword,
            cancellationToken: context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        Projection.UserAuthentication userAuthentication = new(context.Message.UserId, context.Message.Email, context.Message.Password, false);
        return _repository.InsertAsync(userAuthentication, context.CancellationToken);
    }
}