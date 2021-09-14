using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts.Queries;

namespace Application.UseCases.Queries
{
    public class GetAccountDetailsConsumer : IConsumer<GetAccountDetails>
    {
        private readonly IAccountProjectionsService _projectionsService;

        public GetAccountDetailsConsumer(IAccountProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetAccountDetails> context)
        {
            var accountDetails = await _projectionsService.GetAccountDetailsAsync(context.Message.Id, context.CancellationToken);
            await context.RespondAsync(accountDetails);
        }
    }
}