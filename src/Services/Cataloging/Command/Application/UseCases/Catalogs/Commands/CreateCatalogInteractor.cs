using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.Catalogs;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record CreateCatalog(AppId AppId, Title Title, Description Description) : IRequest<CatalogId>;

public class CreateCatalogInteractor(IApplicationService service) : IRequestHandler<CreateCatalog, CatalogId>
{
    public async Task<CatalogId> Handle(CreateCatalog cmd, CancellationToken cancellationToken)
    {
        var newCatalog = Catalog.Create(cmd.AppId, cmd.Title, cmd.Description);
        await service.AppendEventsAsync<Catalog, CatalogId>(newCatalog, cancellationToken);
        return newCatalog.Id;
    }
}