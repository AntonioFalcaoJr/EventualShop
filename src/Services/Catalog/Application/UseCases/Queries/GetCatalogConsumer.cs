using Application.Abstractions.Projections;
using ECommerce.Abstractions;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCatalogConsumer :
    IConsumer<Query.GetCatalog>,
    IConsumer<Query.GetCatalogs>
{
    private readonly IProjectionRepository<Projection.Catalog> _repository;

    public GetCatalogConsumer(IProjectionRepository<Projection.Catalog> repository)
    {
        _repository = repository;
    }

    public async Task Consume(ConsumeContext<Query.GetCatalog> context)
    {
        var catalog = await _repository.GetAsync(context.Message.CatalogId, context.CancellationToken);

        await (catalog is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalog));
    }

    public async Task Consume(ConsumeContext<Query.GetCatalogs> context)
    {
        var catalogs = await _repository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogs is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogs));
    }
}