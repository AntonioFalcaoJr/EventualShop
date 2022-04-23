using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogsConsumer : IConsumer<Queries.GetCatalogs>
{
    private readonly IProjectionRepository<Projections.Catalog> _projectionRepository;

    public GetCatalogsConsumer(IProjectionRepository<Projections.Catalog> projectionRepository)
    {
        _projectionRepository = projectionRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalogs> context)
    {
        var catalogs = await _projectionRepository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogs is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogs));
    }
}