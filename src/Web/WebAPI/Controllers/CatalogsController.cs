using AutoMapper;
using ECommerce.Contracts.Catalogs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.DataTransferObjects.Catalogs;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class CatalogsController : ApplicationController
{
    public CatalogsController(IBus bus, IMapper mapper)
        : base(bus, mapper) { }

    [HttpGet]
    [ProducesResponseType(typeof(Outputs.Catalogs), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetCatalogs(int limit, int offset, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCatalogs, Responses.Catalogs, Outputs.Catalogs>(new(limit, offset), cancellationToken);
    
    [HttpGet("{catalogId:guid}")]
    [ProducesResponseType(typeof(Outputs.Catalogs), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetCatalog(Guid catalogId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCatalog, Responses.Catalog, Outputs.Catalog>(new(catalogId), cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateCatalogAsync(Requests.CreateCatalog request, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.CreateCatalog>(new(request.Title, request.Description), cancellationToken);

    [HttpGet("{catalogId:guid}/items")]
    [ProducesResponseType(typeof(Outputs.CatalogItems), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetCatalogItemsAsync([NotEmpty] Guid catalogId, int limit, int offset, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCatalogItems, Responses.CatalogItems, Outputs.CatalogItems>(new(catalogId, limit, offset), cancellationToken);

    [HttpPost]
    public Task<IActionResult> AddCatalogItem(Commands.AddCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> RemoveCatalogItem(Commands.RemoveCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> ActivateCatalog(Commands.ActivateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> DeactivateCatalog(Commands.DeactivateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateCatalog(Commands.UpdateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateCatalogItem(Commands.UpdateCatalogItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete("{catalogId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteCatalog([NotEmpty] Guid catalogId, CancellationToken cancellationToken)
        => SendCommandAsync<Commands.DeleteCatalog>(new(catalogId), cancellationToken);
}