using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;

namespace Application.UseCases.Commands;

public interface ICreateCatalogInteractor : IInteractor<Command.CreateCatalog> { }

public class CreateCatalogInteractor : ICreateCatalogInteractor
{
    private readonly IApplicationService _applicationService;

    public CreateCatalogInteractor(IApplicationService applicationService)
    {
        _applicationService = applicationService;
    }

    public async Task InteractAsync(Command.CreateCatalog command, CancellationToken cancellationToken)
    {
        Catalog catalog = new();
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}