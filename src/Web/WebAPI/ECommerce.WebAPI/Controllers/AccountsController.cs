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

        [HttpPost]
        public Task<IActionResult> DefineAccountOwner(Commands.DefineAccountOwnerCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPost]
        public Task<IActionResult> AddNewAccountOwnerAddress(Commands.AddNewAccountOwnerAddressCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPost]
        public Task<IActionResult> AddNewAccountOwnerCreditCard(Commands.AddNewAccountOwnerCreditCardCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> ChangeAccountPassword(Commands.ChangeAccountPasswordCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> UpdateAccountOwnerAddress(Commands.UpdateAccountOwnerAddressCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
        
        [HttpPut]
        public Task<IActionResult> UpdateAccountOwnerCreditCard(Commands.UpdateAccountOwnerCreditCardCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
        
        [HttpPut]
        public Task<IActionResult> UpdateAccountOwnerDetails(Commands.UpdateAccountOwnerCreditCardCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpDelete]
        public Task<IActionResult> DeleteAccount(Commands.DeleteAccountCommand command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}