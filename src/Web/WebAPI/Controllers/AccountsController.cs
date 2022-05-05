using Contracts.Abstractions.Paging;
using Contracts.DataTransferObjects;
using Contracts.Services.Account;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;
using WebAPI.ValidationAttributes;

namespace WebAPI.Controllers;

public class AccountsController : ApplicationController
{
    public AccountsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    [ProducesResponseType(typeof(IPagedResult<Projection.Account>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync(int limit, int offset, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetAccounts, IPagedResult<Projection.Account>>(new(limit, offset), cancellationToken);

    [HttpGet("{accountId:guid}")]
    [ProducesResponseType(typeof(Projection.Account), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> GetAsync([NotEmpty] Guid accountId, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetAccount, Projection.Account>(new(accountId), cancellationToken);

    [HttpDelete("{accountId:guid}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DeleteAsync([NotEmpty] Guid accountId, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DeleteAccount>(new(accountId), cancellationToken);

    [HttpPut("{accountId:guid}/profiles/professional-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DefineProfessionalAddressAsync([NotEmpty] Guid accountId, Dto.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DefineProfessionalAddress>(new(accountId, address), cancellationToken);

    [HttpPut("{accountId:guid}/profiles/residence-address")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public Task<IActionResult> DefineResidenceAddressAsync([NotEmpty] Guid accountId, Dto.Address address, CancellationToken cancellationToken)
        => SendCommandAsync<Command.DefineResidenceAddress>(new(accountId, address), cancellationToken);
}