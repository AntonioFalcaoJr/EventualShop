using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Catalog;
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

        await context.RespondAsync(catalogItems switch
        {
            {PageInfo.Size: > 0} => catalogItems,
            {PageInfo.Size: <= 0} => new NoContent(),
            _ => new NotFound()
        });
    }

    public async Task Consume(ConsumeContext<Query.GetAllItems> context)
    {
        var items = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(items switch
        {
            {PageInfo.Size: > 0} => items,
            {PageInfo.Size: <= 0} => new NoContent(),
            _ => new NotFound()
        });
    }
}