using Application.Services;
using Domain.Aggregates;
using Domain.Aggregates.Catalogs;
using Domain.ValueObjects;
using MediatR;

namespace Application.UseCases.Catalogs.Commands;

public record RegisterCatalog(AppId AppId, Title Title, Description Description) : IRequest<CatalogId>;

public class RegisterCatalogInteractor(IApplicationService service) : IRequestHandler<RegisterCatalog, CatalogId>
{
    public async Task<CatalogId> Handle(RegisterCatalog cmd, CancellationToken cancellationToken)
    {
        Catalog catalog = new();
        catalog.Register(cmd.AppId, cmd.Title, cmd.Description);
        await service.AppendEventsAsync<Catalog, CatalogId>(catalog, cancellationToken);
        return catalog.Id;
    }
}