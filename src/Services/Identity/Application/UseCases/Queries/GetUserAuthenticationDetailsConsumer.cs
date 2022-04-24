using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.Identities;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetUserAuthenticationDetailsConsumer : IConsumer<Query.GetUserAuthentication>
{
    private readonly IProjectionRepository<Projection.UserAuthentication> _repository;

    public GetUserAuthenticationDetailsConsumer(IProjectionRepository<Projection.UserAuthentication> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetUserAuthentication> context)
    {
        var userAuthentication = await _repository.GetAsync(context.Message.UserId, context.CancellationToken);
        
        await (userAuthentication is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(userAuthentication));
    }
}