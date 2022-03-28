using AutoMapper;
using ECommerce.Contracts.Catalog;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class CatalogsController : ApplicationController
{
    public CatalogsController(IBus bus, IMapper mapper)
        : base(bus, mapper) { }

    [HttpGet]
    public Task<IActionResult> GetCatalogItemsWithPagination([FromQuery] Queries.GetCatalogItems query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetCatalogItems, Responses.CatalogItemsDetailsPagedResult>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateCatalog(Commands.CreateCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

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

    [HttpDelete]
    public Task<IActionResult> DeleteCatalog(Commands.DeleteCatalog command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}