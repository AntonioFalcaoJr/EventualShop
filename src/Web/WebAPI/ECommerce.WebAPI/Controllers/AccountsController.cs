using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using ECommerce.WebAPI.Messages.Accounts;
using MassTransit;
using Messages.Accounts.Queries;
using Messages.Accounts.Queries.Responses;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class AccountsController : ApplicationController
    {
        public AccountsController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetAccountsWithPagination([FromQuery] Queries.GetAccountsDetailsWithPaginationQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetAccountsDetailsWithPagination, AccountsDetailsPagedResult>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAccountDetails([FromQuery] Queries.GetAccountDetailsQuery query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetAccountDetails, AccountDetails>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterAccount(Commands.RegisterAccountCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateAccount(Commands.UpdateAccountCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteAccount(Commands.DeleteAccountCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}