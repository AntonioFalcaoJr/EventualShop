using Application.Services;
using Domain.Aggregates.Catalogs;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record ChangeCatalogTitle(CatalogId CatalogId, Title NewTitle) : IRequest;

public class ChangeCatalogTitleInteractor(IApplicationService service) : IRequestHandler<ChangeCatalogTitle>
{
    public async Task Handle(ChangeCatalogTitle cmd, CancellationToken cancellationToken)
    {
        var catalog = await service.LoadAggregateAsync<Catalog, CatalogId>(cmd.CatalogId, cancellationToken);
        catalog.ChangeCatalogTitle(cmd.NewTitle);
        await service.AppendEventsAsync<Catalog, CatalogId>(catalog, cancellationToken);
    }
}