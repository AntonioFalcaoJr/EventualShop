using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogConsumer : IConsumer<Queries.GetCatalog>
{
    private readonly IProjectionRepository<Projection.Catalog> _projectionRepository;

    public GetCatalogConsumer(IProjectionRepository<Projection.Catalog> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalog> context)
    {
        var catalog = await _projectionRepository.GetAsync(context.Message.CatalogId, context.CancellationToken);

        await (catalog is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalog));
    }
}