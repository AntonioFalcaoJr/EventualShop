using System;
using Messages.Customers.Commands;

namespace ECommerce.WebAPI.Messages.Commands
{
    public static class Commands
    {
        public record RegisterCustomerCommand(string Name, int Age) : RegisterCustomer;
        public record UpdateCustomerCommand(Guid Id, string Name, int Age) : UpdateCustomer;
        public record DeleteCustomerCommand(Guid Id) : DeleteCustomer;
    }
}