using System.Threading.Tasks;
using Application.EventSourcing.EventStore;
using Application.EventSourcing.Projections;
using MassTransit;

namespace Application.UseCases.Events.Projections
{
    public class ProfessionalAddressDefinedConsumer : IConsumer<Messages.Accounts.Events.ProfessionalAddressDefined>
    {
        private readonly IAccountEventStoreService _eventStoreService;
        private readonly IAccountProjectionsService _projectionsService;

        public ProfessionalAddressDefinedConsumer(IAccountEventStoreService eventStoreService, IAccountProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Messages.Accounts.Events.ProfessionalAddressDefined> context)
        {
            var account = await _eventStoreService.LoadAggregateFromStreamAsync(context.Message.AccountId, context.CancellationToken);

            var accountDetails = new AccountDetailsProjection
            {
                Id = account.Id,
                Profile = new ProfileProjection
                {
                    ProfessionalAddress = new AddressProjection
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

            await _projectionsService.UpdateAccountProfileAsync(accountDetails, context.CancellationToken);
        }
    }
}