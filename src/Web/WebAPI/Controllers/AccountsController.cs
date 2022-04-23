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
    public Task<IActionResult> GetAccountsWithPagination([FromQuery] Queries.GetAccounts query, CancellationToken cancellationToken)
        => GetProjectionAsync<Queries.GetAccounts, IPagedResult<Projections.Account>>(query, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetAccountDetails([FromQuery] Queries.GetAccountDetails query, CancellationToken cancellationToken)
        => GetProjectionAsync<Queries.GetAccountDetails, Projections.Account>(query, cancellationToken);

    [HttpPost]
    public Task<IActionResult> CreateAccount(Commands.CreateAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineProfessionalAddress(Commands.DefineProfessionalAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPost]
    public Task<IActionResult> DefineResidenceAddress(Commands.DefineResidenceAddress command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpPut]
    public Task<IActionResult> UpdateProfile(Commands.UpdateProfile command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);

    [HttpDelete]
    public Task<IActionResult> DeleteAccount(Commands.DeleteAccount command, CancellationToken cancellationToken)
        => SendCommandAsync(command, cancellationToken);
}