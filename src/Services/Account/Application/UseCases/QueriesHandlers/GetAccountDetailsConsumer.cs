using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts;

namespace Application.UseCases.QueriesHandlers
{
    public class GetAccountDetailsConsumer : IConsumer<Queries.GetAccountDetails>
    {
        private readonly IAccountProjectionsService _projectionsService;

        public GetAccountDetailsConsumer(IAccountProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Queries.GetAccountDetails> context)
        {
            var accountDetails = await _projectionsService.GetAccountDetailsAsync(context.Message.Id, context.CancellationToken);
            await context.RespondAsync(accountDetails);
        }
    }
}