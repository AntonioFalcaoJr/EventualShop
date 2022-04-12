using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using ECommerce.Contracts.Accounts;
using MassTransit;

namespace Application.UseCases.EventHandlers.Projections;

public class ProjectAccountDetailsWhenChangedConsumer :
    IConsumer<DomainEvents.AccountCreated>,
    IConsumer<DomainEvents.AccountDeleted>,
    IConsumer<DomainEvents.ProfessionalAddressDefined>,
    IConsumer<DomainEvents.ProfileUpdated>,
    IConsumer<DomainEvents.ResidenceAddressDefined>
{
    private readonly IAccountEventStoreService _eventStoreService;
    private readonly IAccountProjectionsService _projectionsService;

    public ProjectAccountDetailsWhenChangedConsumer(
        IAccountEventStoreService eventStoreService,
        IAccountProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public Task Consume(ConsumeContext<DomainEvents.AccountCreated> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.AccountDeleted> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.ProfessionalAddressDefined> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.ProfileUpdated> context)
        => ProjectAsync(context.Message.AccountId, context.CancellationToken);

    public Task Consume(ConsumeContext<DomainEvents.ResidenceAddressDefined> context)
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