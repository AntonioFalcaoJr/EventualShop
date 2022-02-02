using System;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Contracts.Warehouse;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[Route("api/v1/[controller]")]
public class WarehousesController : ApplicationController
{
    public WarehousesController(IBus bus)
        : base(bus) { }

    [HttpGet("{productId:guid}")]
    public Task<IActionResult> GetInventoryItemDetailsAsync(Guid productId, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetInventoryItemDetails, Responses.InventoryItemDetails, Responses.NotFound>(new(productId), cancellationToken);

    [HttpPost]
    public Task<IActionResult> ReceiveInventoryItemAsync(Commands.ReceiveInventoryItem command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut("{productId:guid}/[action]")]
    public Task<IActionResult> AdjustInventoryAsync(Guid productId, Commands.AdjustInventory command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}