using Application.Abstractions.Projections;
using Contracts.Abstractions;
using Contracts.Services.Catalog;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCatalogConsumer : IConsumer<Query.GetCatalog>, IConsumer<Query.GetCatalogs>
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
            ? context.RespondAsync<Reply.NotFound>(new())
            : context.RespondAsync(catalog));
    }

    public async Task Consume(ConsumeContext<Query.GetCatalogs> context)
    {
        var catalogs = await _repository.GetAsync(
            limit: context.Message.Limit,
            offset: context.Message.Offset,
            cancellationToken: context.CancellationToken);

        await context.RespondAsync(catalogs switch
        {
            {Page.Size: > 0} => catalogs,
            {Page.Size: < 1} => new Reply.NoContent(),
            _ => new Reply.NotFound()
        });
    }
}