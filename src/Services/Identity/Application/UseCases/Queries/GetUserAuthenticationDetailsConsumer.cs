using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Identities.Queries;
using Messages.Identities.Queries.Responses;

namespace Application.UseCases.Queries
{
    public class GetUserAuthenticationDetailsConsumer : IConsumer<GetUserAuthenticationDetails>
    {
        private readonly IUserProjectionsService _projectionsService;

        public GetUserAuthenticationDetailsConsumer(IUserProjectionsService projectionsService)
        {
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<GetUserAuthenticationDetails> context)
        {
            // TODO - Multiple responses
            var userAuthenticationDetails = await _projectionsService.GetUserAuthenticationDetailsAsync(context.Message.UserId, context.CancellationToken);
            await context.RespondAsync<UserAuthenticationDetails>(userAuthenticationDetails);
        }
    }
}