using Application.Services;
using Domain.Aggregates.Catalogs;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record ChangeCatalogDescription(CatalogId CatalogId, Description NewDescription) : IRequest;

public class ChangeCatalogDescriptionInteractor(IApplicationService service) : IRequestHandler<ChangeCatalogDescription>
{
    public async Task Handle(ChangeCatalogDescription cmd, CancellationToken cancellationToken)
    {
        var catalog = await service.LoadAggregateAsync<Catalog, CatalogId>(cmd.CatalogId, cancellationToken);
        catalog.ChangeDescription(cmd.NewDescription);
        await service.AppendEventsAsync<Catalog, CatalogId>(catalog, cancellationToken);
    }
}