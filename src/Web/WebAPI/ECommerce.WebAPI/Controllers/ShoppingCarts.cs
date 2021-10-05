using System.Threading;
using System.Threading.Tasks;
using ECommerce.WebAPI.Abstractions;
using MassTransit;
using Messages.ShoppingCarts;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.WebAPI.Controllers
{
    public class ShoppingCarts : ApplicationController
    {
        public ShoppingCarts(IBus bus)
            : base(bus) { }

        [HttpPost]
        public Task<IActionResult> CreateCart(Commands.CreateCart command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);

        [HttpPut]
        public Task<IActionResult> AddCartItem(Commands.AddCartItem command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
        
        [HttpPut]
        public Task<IActionResult> RemoveCartItem(Commands.RemoveCartItem command, CancellationToken cancellationToken)
            => SendCommandAsync(command, cancellationToken);
    }
}