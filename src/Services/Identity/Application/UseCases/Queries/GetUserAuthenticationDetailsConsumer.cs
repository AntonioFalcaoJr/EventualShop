using System.Threading.Tasks;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Identities;
using GetUserAuthenticationDetailsQuery = Messages.Identities.Queries.GetUserAuthenticationDetails;

namespace Application.UseCases.Queries;

public class GetUserAuthenticationDetailsConsumer : IConsumer<GetUserAuthenticationDetailsQuery>
{
    private readonly IUserProjectionsService _projectionsService;

    public GetUserAuthenticationDetailsConsumer(IUserProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<GetUserAuthenticationDetailsQuery> context)
    {
        // TODO - Multiple responses
        var userAuthenticationDetails = await _projectionsService.GetUserAuthenticationDetailsAsync(context.Message.UserId, context.CancellationToken);
        await context.RespondAsync<Responses.UserAuthenticationDetails>(userAuthenticationDetails);
    }
}