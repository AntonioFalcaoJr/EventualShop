using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogsConsumer : IConsumer<Query.GetCatalogs>
{
    private readonly IProjectionRepository<Projection.Catalog> _projectionRepository;

    public GetCatalogsConsumer(IProjectionRepository<Projection.Catalog> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Query.GetCatalogs> context)
    {
        var catalogs = await _projectionRepository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogs is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogs));
    }
}