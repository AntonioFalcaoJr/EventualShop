using Application.Abstractions.Projections;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.Events.Projections;

public class ProjectAccountDetailsWhenChangedConsumer :
    IConsumer<DomainEvent.AccountCreated>,
    IConsumer<DomainEvent.AccountDeleted>,
    IConsumer<DomainEvent.ProfessionalAddressDefined>,
    IConsumer<DomainEvent.ProfileUpdated>,
    IConsumer<DomainEvent.ResidenceAddressDefined>
{
    private readonly IProjectionRepository<Projection.Account> _repository;

    public ProjectAccountDetailsWhenChangedConsumer(IProjectionRepository<Projection.Account> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<DomainEvent.AccountCreated> context)
    {
        var account = new Projection.Account
        {
            Id = context.Message.AccountId,
            UserId = context.Message.UserId,
            Profile = new()
            {
                Email = context.Message.Email
            },
            IsDeleted = false
        };

        await _repository.InsertAsync(account, context.CancellationToken);
    }

    public async Task Consume(ConsumeContext<DomainEvent.AccountDeleted> context)
        => await _repository.DeleteAsync(context.Message.AccountId, context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.ProfessionalAddressDefined> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile.ProfessionalAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);

    // TODO - Improve this, update all profile dont like the right approach
    public async Task Consume(ConsumeContext<DomainEvent.ProfileUpdated> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile,
            value: new()
            {
                Birthdate = context.Message.Birthdate,
                Email = context.Message.Email,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName
            },
            cancellationToken: context.CancellationToken);

    public async Task Consume(ConsumeContext<DomainEvent.ResidenceAddressDefined> context)
        => await _repository.UpdateFieldAsync(
            id: context.Message.AccountId,
            field: account => account.Profile.ResidenceAddress,
            value: context.Message.Address,
            cancellationToken: context.CancellationToken);
}