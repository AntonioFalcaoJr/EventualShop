using Application.Abstractions.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogConsumer : IConsumer<Queries.GetCatalog>
{
    private readonly IProjectionsRepository<Projections.Catalog> _projectionsRepository;

    public GetCatalogConsumer(IProjectionsRepository<Projections.Catalog> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalog> context)
    {
        var catalog = await _projectionsRepository.GetAsync(context.Message.CatalogId, context.CancellationToken);

        await (catalog is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalog));
    }
}