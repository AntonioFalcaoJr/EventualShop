using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UseCases.Customers.Queries.GetCustomers;
using Domain.Entities.Customers;

namespace Application.Interfaces.Customers
{
    public interface ICustomerProjectionService
    {
        Task<IEnumerable<Models.CustomersModel>> GetCustomersAsync();
        Task ProjectAsync(Customer customer);
    }
}