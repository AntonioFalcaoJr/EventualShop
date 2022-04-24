using ECommerce.Contracts.Warehouses;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/v1/[controller]")]
public class WarehousesController : ApplicationController
{
    public WarehousesController(IBus bus)
        : base(bus) { }

    [HttpGet("{productId:guid}")]
    public Task<IActionResult> GetInventoryItemDetailsAsync(Guid productId, CancellationToken cancellationToken)
        => GetProjectionAsync<Queries.GetInventoryItemDetails, Projection.Inventory>(new(productId), cancellationToken);

    [HttpPost]
    public Task<IActionResult> ReceiveInventoryItemAsync(Command.ReceiveInventoryItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut("{productId:guid}/[action]")]
    public Task<IActionResult> AdjustInventoryAsync(Guid productId, Command.AdjustInventory command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}