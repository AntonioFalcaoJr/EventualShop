using System.Threading.Tasks;
using Application.EventSourcing.Catalogs.Projections;
using MassTransit;
using Messages.Accounts.Queries;

namespace Application.UseCases.Catalogs.Queries
{
    public class GetAccountDetailsConsumer : IConsumer<GetAccountDetails>
    {
        private readonly ICatalogProjectionsService _projectionsService;

        public GetAccountDetailsConsumer(ICatalogProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetAccountDetails> context)
        {
            var accountDetails = await _projectionsService.GetCatalogDetailsAsync(context.Message.Id, context.CancellationToken);
            await context.RespondAsync(accountDetails);
        }
    }
}