using System;
using System.Threading;
using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using AccountCreatedEvent = Messages.Services.Accounts.DomainEvents.AccountCreated;
using AccountDeletedEvent = Messages.Services.Accounts.DomainEvents.AccountDeleted;
using ProfessionalAddressDefinedEvent = Messages.Services.Accounts.DomainEvents.ProfessionalAddressDefined;
using ProfileUpdatedEvent = Messages.Services.Accounts.DomainEvents.ProfileUpdated;
using ResidenceAddressDefinedEvent = Messages.Services.Accounts.DomainEvents.ResidenceAddressDefined;

namespace Application.UseCases.Events.Projections;

public class ProjectAccountDetailsWhenAccountChangedConsumer :
    IConsumer<AccountCreatedEvent>,
    IConsumer<AccountDeletedEvent>,
    IConsumer<ProfessionalAddressDefinedEvent>,
    IConsumer<ProfileUpdatedEvent>,
    IConsumer<ResidenceAddressDefinedEvent>
{
    private readonly IAccountEventStoreService _eventStoreService;
    private readonly IAccountProjectionsService _projectionsService;

    public ProjectAccountDetailsWhenAccountChangedConsumer(
        IAccountEventStoreService eventStoreService,
        IAccountProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<AccountCreatedEvent> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<AccountDeletedEvent> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<ProfessionalAddressDefinedEvent> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<ProfileUpdatedEvent> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<ResidenceAddressDefinedEvent> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    private async Task ProjectAsync(Guid accountId, CancellationToken cancellationToken)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(accountId, cancellationToken);

        var accountDetails = new AccountDetailsProjection
        {
            Id = account.Id,
            UserId = account.UserId,
            Profile = new()
            {
                Birthdate = account.Profile.Birthdate,
                Email = account.Profile.Email,
                FirstName = account.Profile.FirstName,
                LastName = account.Profile.LastName,
                ProfessionalAddress = new()
                {
                    City = account.Profile.ProfessionalAddress.City,
                    Country = account.Profile.ProfessionalAddress.Country,
                    Number = account.Profile.ProfessionalAddress.Number,
                    State = account.Profile.ProfessionalAddress.State,
                    Street = account.Profile.ProfessionalAddress.Street,
                    ZipCode = account.Profile.ProfessionalAddress.ZipCode
                },
                ResidenceAddress = new()
                {
                    City = account.Profile.ResidenceAddress.City,
                    Country = account.Profile.ResidenceAddress.Country,
                    Number = account.Profile.ResidenceAddress.Number,
                    State = account.Profile.ResidenceAddress.State,
                    Street = account.Profile.ResidenceAddress.Street,
                    ZipCode = account.Profile.ResidenceAddress.ZipCode
                }
            },
            IsDeleted = account.IsDeleted
        };

        await _projectionsService.ProjectAsync(accountDetails, cancellationToken);
    }
}