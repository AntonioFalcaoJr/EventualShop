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
            => GetQueryResponseAsync<GetAccountsDetailsWithPagination, IPagedResult<AccountAuthenticationDetailsProjection>>(query, cancellationToken);

        [HttpGet]
        public Task<IActionResult> GetAccountAuthenticationDetails([FromQuery] GetAccountDetails query, CancellationToken cancellationToken)
            => GetQueryResponseAsync<GetAccountDetails, AccountAuthenticationDetailsProjection>(query, cancellationToken);

        [HttpPost]
        public Task<IActionResult> RegisterAccount(RegisterAccount command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> ChangeAccountPassword(ChangeAccountPassword command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteAccount(DeleteAccount command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}