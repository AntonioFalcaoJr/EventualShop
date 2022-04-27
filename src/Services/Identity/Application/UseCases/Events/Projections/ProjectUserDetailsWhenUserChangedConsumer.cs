using Application.Abstractions.Projections;
using Contracts.Services.Identities;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectUserDetailsWhenUserChangedConsumer :
    IConsumer<DomainEvent.UserRegistered>,
    IConsumer<DomainEvent.UserPasswordChanged>,
    IConsumer<DomainEvent.UserDeleted>
{
    private readonly IProjectionRepository<Projection.UserAuthentication> _repository;

    public ProjectUserDetailsWhenUserChangedConsumer(IProjectionRepository<Projection.UserAuthentication> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.UserDeleted> context)
        => await _repository.DeleteAsync(context.Message.UserId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.UserPasswordChanged> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.UserId,
            field: user => user.Password,
            value: context.Message.NewPassword,
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.UserRegistered> context)
    {
        var userAuthentication = new Projection.UserAuthentication
        {
            Id = context.Message.UserId,
            Email = context.Message.Email,
            Password = context.Message.Password,
            IsDeleted = false
        };

        await _repository.InsertAsync(userAuthentication, context.CancellationToken);
    }
}