using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCatalogItemsConsumer : 
    IConsumer<Query.GetCatalogItems>,
    IConsumer<Query.GetAllItems>
{
    private readonly IProjectionRepository<Projection.CatalogItem> _repository;

    public GetCatalogItemsConsumer(IProjectionRepository<Projection.CatalogItem> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetCatalogItems> context)
    {
        var catalogItems = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            predicate: item => item.CatalogId == context.Message.CatalogId,
            cancellationToken: context.CancellationToken);

        await (catalogItems is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogItems));
    }
    
    public async Task Consume(ConsumeContext<Query.GetAllItems> context)
    {
        var items = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await (items is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(items));
    }
}