using Contracts.Abstractions.Paging;
using Contracts.Services.Warehouse;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.Validations;

namespace WebAPI.Controllers;

public class WarehousesController : ApplicationController
{
    public WarehousesController(IBus bus)
        : base(bus) { }

    [HttpGet]
    [ProducesResponseType(typeof(IPagedResult<Projection.Inventory>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetInventoriesAsync(ushort limit, ushort offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetInventories, IPagedResult<Projection.Inventory>>(new(limit, offset), cancellationToken);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> CreateAsync(Request.CreateInventory request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.CreateInventory>(new(request.OwnerId), cancellationToken);

    [HttpGet("{inventoryId:guid}/items")]
    [ProducesResponseType(typeof(IPagedResult<Projection.InventoryItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetInventoryItemAsync(Guid inventoryId, ushort limit, ushort offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetInventoryItems, IPagedResult<Projection.InventoryItem>>(new(inventoryId, limit, offset), cancellationToken);

    [HttpPost("{inventoryId:guid}/items")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ReceiveInventoryItemAsync([NotEmpty] Guid inventoryId, Request.ReceiveInventoryItem request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ReceiveInventoryItem>(new(inventoryId, request.Product, request.Cost, request.Quantity), cancellationToken);

    [HttpPut("{inventoryId:guid}/items/{inventoryItemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> IncreaseAdjustAsync([NotEmpty] Guid inventoryId, [NotEmpty] Guid inventoryItemId, Request.IncreaseInventoryAdjust request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.IncreaseInventoryAdjust>(new(inventoryId, inventoryItemId, request.Quantity, request.Reason), cancellationToken);

    [HttpPut("{inventoryId:guid}/items/{inventoryItemId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DecreaseAdjustAsync([NotEmpty] Guid inventoryId, [NotEmpty] Guid inventoryItemId, Request.IncreaseInventoryAdjust request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DecreaseInventoryAdjust>(new(inventoryId, inventoryItemId, request.Quantity, request.Reason), cancellationToken);
}