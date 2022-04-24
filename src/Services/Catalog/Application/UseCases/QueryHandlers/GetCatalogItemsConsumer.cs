using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogItemsConsumer : IConsumer<Queries.GetCatalogItems>
{
    private readonly IProjectionRepository<Projection.CatalogItem> _projectionRepository;

    public GetCatalogItemsConsumer(IProjectionRepository<Projection.CatalogItem> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalogItems> context)
    {
        var catalogItems = await _projectionRepository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            predicate: item => item.CatalogId == context.Message.CatalogId,
            cancellationToken: context.CancellationToken);

        await (catalogItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogItems));
    }
}