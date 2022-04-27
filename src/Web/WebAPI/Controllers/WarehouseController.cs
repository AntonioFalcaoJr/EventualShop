using Contracts.Services.Warehouse;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

public class WarehousesController : ApplicationController
{
    public WarehousesController(IBus bus)
        : base(bus) { }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> ReceiveInventoryItemAsync(Request.ReceiveInventoryItem request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.ReceiveInventoryItem>(new(request.Product, request.Quantity), cancellationToken);
    
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(Projection.Inventory), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetInventoryItemAsync(Guid productId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetInventoryItemDetails, Projection.Inventory>(new(productId), cancellationToken);

    [HttpPut("{productId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> IncreaseAdjustAsync([NotEmpty] Guid productId, Request.IncreaseInventoryAdjust request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.IncreaseInventoryAdjust>(new(productId, request.Quantity, request.Reason), cancellationToken);

    [HttpPut("{productId:guid}/[action]")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DecreaseAdjustAsync([NotEmpty] Guid productId, Request.DecreaseInventoryAdjust request, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DecreaseInventoryAdjust>(new(productId, request.Quantity, request.Reason), cancellationToken);
}