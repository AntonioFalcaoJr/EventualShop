using Contracts.Abstractions.Paging;
using Contracts.Services.Catalogs;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

public class CatalogsController : ApplicationController
{
    public CatalogsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    [ProducesResponseType(typeof(IPagedResult<Projection.Catalog>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync(int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetCatalogs, IPagedResult<Projection.Catalog>>(new(limit, offset), cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(Request.CreateCatalog request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.CreateCatalog>(new(request.CatalogId, request.Title, request.Description), cancellationToken);

    [HttpGet("{catalogId:guid}")]
    [ProducesResponseType(typeof(Projection.Catalog), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid catalogId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetCatalog, Projection.Catalog>(new(catalogId), cancellationToken);

    [HttpDelete("{catalogId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteAsync([NotEmpty] Guid catalogId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DeleteCatalog>(new(catalogId), cancellationToken);

    [HttpPut("{catalogId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ActivateAsync([NotEmpty] Guid catalogId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ActivateCatalog>(new(catalogId), cancellationToken);

    [HttpPut("{catalogId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeactivateAsync([NotEmpty] Guid catalogId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DeactivateCatalog>(new(catalogId), cancellationToken);

    [HttpPut("{catalogId:guid}/title")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangeTitleAsync([NotEmpty] Guid catalogId, Request.ChangeCatalogTitle request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ChangeCatalogTitle>(new(catalogId, request.Title), cancellationToken);

    [HttpPut("{catalogId:guid}/description")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ChangeDescriptionAsync([NotEmpty] Guid catalogId, Request.ChangeCatalogDescription request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ChangeCatalogDescription>(new(catalogId, request.Description), cancellationToken);

    [HttpGet("items")]
    [ProducesResponseType(typeof(IPagedResult<Projection.CatalogItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAllAsync(int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetAllItems, IPagedResult<Projection.CatalogItem>>(new(limit, offset), cancellationToken);

    [HttpGet("{catalogId:guid}/items")]
    [ProducesResponseType(typeof(IPagedResult<Projection.CatalogItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid catalogId, int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetCatalogItems, IPagedResult<Projection.CatalogItem>>(new(catalogId, limit, offset), cancellationToken);

    [HttpPost("{catalogId:guid}/items")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> AddAsync([NotEmpty] Guid catalogId, Request.AddCatalogItem request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.AddCatalogItem>(new(catalogId, request.Product, request.Quantity), cancellationToken);

    [HttpDelete("{catalogId:guid}/items/{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteAsync([NotEmpty] Guid catalogId, [NotEmpty] Guid itemId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DeleteCatalogItem>(new(catalogId, itemId), cancellationToken);
}