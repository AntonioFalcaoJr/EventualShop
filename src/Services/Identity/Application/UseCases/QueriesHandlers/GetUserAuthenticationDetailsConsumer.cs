using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Identities;

namespace Application.UseCases.QueriesHandlers
{
    public class GetUserAuthenticationDetailsConsumer : IConsumer<Queries.GetUserAuthenticationDetails>
    {
        private readonly IUserProjectionsService _projectionsService;

        public GetUserAuthenticationDetailsConsumer(IUserProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Queries.GetUserAuthenticationDetails> context)
        {
            // TODO - Multiple responses
            var userAuthenticationDetails = await _projectionsService.GetUserAuthenticationDetailsAsync(context.Message.UserId, context.CancellationToken);
            await context.RespondAsync<Responses.UserAuthenticationDetails>(userAuthenticationDetails);
        }
    }
}