using System.Threading;
using System.Threading.Tasks;
using ECommerce.Contracts.Account;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers;

[Route("api/[controller]/[action]")]
public class AccountsController : ApplicationController
{
    public AccountsController(IBus bus)
        : base(bus) { }

    [HttpGet]
    public Task<IActionResult> GetAccountsWithPagination([FromQuery] Queries.GetAccountsDetailsWithPagination query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetAccountsDetailsWithPagination, Responses.AccountsDetailsPagedResult, Responses.NotFound>(query, cancellationToken);

    [HttpGet]
    public Task<IActionResult> GetAccountDetails([FromQuery] Queries.GetAccountDetails query, CancellationToken cancellationToken)
        => GetResponseAsync<Queries.GetAccountDetails, Responses.AccountDetails, Responses.NotFound>(query, cancellationToken);

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