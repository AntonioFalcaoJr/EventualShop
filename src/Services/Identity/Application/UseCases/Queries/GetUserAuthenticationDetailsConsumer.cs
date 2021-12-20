using Application.EventSourcing.Projections;
using ECommerce.Contracts.Identity;
using MassTransit;
using GetUserAuthenticationDetailsQuery = ECommerce.Contracts.Identity.Queries.GetUserAuthenticationDetails;

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