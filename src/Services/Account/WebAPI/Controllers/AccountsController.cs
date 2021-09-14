using System.Threading;
using System.Threading.Tasks;
using Application.Abstractions.EventSourcing.Projections.Pagination;
using Application.EventSourcing.Projections;
using MassTransit;
using Messages.Accounts.Commands;
using Messages.Accounts.Queries;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Abstractions;

namespace WebAPI.Controllers
{
    public class AccountsController : ApplicationController
    {
        public AccountsController(IBus bus)
            : base(bus) { }

        [HttpGet]
        public Task<IActionResult> GetAccountsWithPagination([FromQuery] GetAccountsDetailsWithPagination query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetAccountsDetailsWithPagination, IPagedResult<AccountDetailsProjection>>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAccountDetails([FromQuery] GetAccountDetails query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetAccountDetails, AccountDetailsProjection>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterAccount(RegisterAccount command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateAccount(UpdateAccount command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteAccount(DeleteAccount command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}