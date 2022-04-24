using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetUserAuthenticationDetailsConsumer : IConsumer<Queries.GetUserAuthenticationDetails>
{
    private readonly IProjectionRepository<Projection.UserAuthentication> _repository;

    public GetUserAuthenticationDetailsConsumer(IProjectionRepository<Projection.UserAuthentication> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Queries.GetUserAuthenticationDetails> context)
    {
        var userAuthentication = await _repository.GetAsync(context.Message.UserId, context.CancellationToken);
        
        await (userAuthentication is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(userAuthentication));
    }
}