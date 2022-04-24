using ECommerce.Abstractions.Messages.Queries.Paging;
using ECommerce.Contracts.Accounts;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class AccountsController : ApplicationController
{
    public AccountsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetAccountsWithPagination([FromQuery] Query.GetAccounts query, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetAccounts, IPagedResult<Projection.Account>>(query, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetAccountDetails([FromQuery] Query.GetAccount query, CancellationToken cancellationToken)
        => GetProjectionAsync<Query.GetAccount, Projection.Account>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateAccount(Command.CreateAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineProfessionalAddress(Command.DefineProfessionalAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineResidenceAddress(Command.DefineResidenceAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateProfile(Command.UpdateProfile command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteAccount(Command.DeleteAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}