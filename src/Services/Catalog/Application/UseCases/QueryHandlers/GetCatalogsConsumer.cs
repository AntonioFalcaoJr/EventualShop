using Application.Abstractions.Projections;
using ECommerce.Abstractions.Messages.Queries.Responses;
using ECommerce.Contracts.Catalogs;
using MassTransit;

namespace Application.UseCases.QueryHandlers;

public class GetCatalogsConsumer : IConsumer<Queries.GetCatalogs>
{
    private readonly IProjectionsRepository<Projections.Catalog> _projectionsRepository;

    public GetCatalogsConsumer(IProjectionsRepository<Projections.Catalog> projectionsRepository)
    {
        _projectionsRepository = projectionsRepository;
    }

    public async Task Consume(ConsumeContext<Queries.GetCatalogs> context)
    {
        var catalogs = await _projectionsRepository.GetAsync(context.Message.Limit, context.Message.Offset, context.CancellationToken);

        await (catalogs is null
            ? context.RespondAsync<NotFound>(new())
            : context.RespondAsync(catalogs));
    }
}