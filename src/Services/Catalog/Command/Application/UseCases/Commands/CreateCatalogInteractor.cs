using Application.Abstractions;
using Application.Services;
using Contracts.Services.Catalog;
using Domain.Aggregates;
using MassTransit;

namespace Application.UseCases.Commands;

public class CreateCatalogInteractor : IInteractor<Command.CreateCatalog>
{
    private readonly IApplicationService _applicationService;

    public CreateCatalogInteractor(IApplicationService applicationService)
        => _applicationService = applicationService;
    
    public async Task InteractAsync(Command.CreateCatalog command, CancellationToken cancellationToken)
    {
        Catalog catalog = new();
        catalog.Handle(command);
        await _applicationService.AppendEventsAsync(catalog, cancellationToken);
    }
}
