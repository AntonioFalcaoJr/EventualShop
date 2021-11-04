using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;
using ProfessionalAddressDefinedEvent = Messages.Services.Accounts.DomainEvents.ProfessionalAddressDefined;

namespace Application.UseCases.Events.Projections;

public class ProfessionalAddressDefinedConsumer : IConsumer<ProfessionalAddressDefinedEvent>
{
    private readonly IAccountEventStoreService _eventStoreService;
    private readonly IAccountProjectionsService _projectionsService;

    public ProfessionalAddressDefinedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
    {
        _eventStoreService = eventStoreService;
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<ProfessionalAddressDefinedEvent> context)
    {
        var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

        var accountDetails = new AccountDetailsProjection
        {
            Id = account.Id,
            Profile = new()
            {
                ProfessionalAddress = new()
                {
                    City = account.Profile.ProfessionalAddress.City,
                    Country = account.Profile.ProfessionalAddress.Country,
                    Number = account.Profile.ProfessionalAddress.Number,
                    State = account.Profile.ProfessionalAddress.State,
                    Street = account.Profile.ProfessionalAddress.Street,
                    ZipCode = account.Profile.ProfessionalAddress.ZipCode
                }
            }
        };

        await _projectionsService.ProjectAsync(accountDetails, context.CancellationToken);
    }
}