using Application.EventSourcing.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.Queries;

public class GetCatalogsConsumer : IConsumer<ECommerce.Contracts.Catalogs.Queries.GetCatalogs>
{
    private readonly ICatalogProjectionsService _projectionsService;

    public GetCatalogsConsumer(ICatalogProjectionsService projectionsService)
    {
        _projectionsService = projectionsService;
    }

    public async Task Consume(ConsumeContext<ECommerce.Contracts.Catalogs.Queries.GetCatalogs> context)
    {
        var catalogs = await _projectionsService.GetCatalogsAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogs is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync<Responses.Catalogs>(catalogs));
    }
}